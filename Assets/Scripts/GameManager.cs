using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton { get; private set; }
    [SerializeField] private LevelFactory levelFactory;
    [SerializeField] private ActionsManager actionsManager;
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private HapticFeedback hapticFeedback;
    [SerializeField] private GameObject winAlert;
    private Bottle selectedBottle;
    private List<Bottle> bottles;

    private void Awake()
    {
        singleton = this;
    }

    public void initialize(Bottle[] bottles)
    {
        this.bottles = new List<Bottle>(bottles);
    }

    public void handleSelection(Bottle newBottle)
    {
        hapticFeedback.vibrate(5);

        if (selectedBottle)
        {
            if (selectedBottle != newBottle && newBottle.canPush(selectedBottle.peekBall()))
            {
                // Ball is able to swap bottles
                swapBalls(selectedBottle, newBottle);
                selectedBottle = null;
            }
            else
                cancelBottleSelection();
        }
        // Set bottle selection
        else if (newBottle.peekBall())
            selectBottle(newBottle);
    }

    public void swapBalls(Bottle oldBottle, Bottle newBottle, bool recordAction = true)
    {
        // Swap bottles
        Ball swapBall = oldBottle.popBall();
        newBottle.forcePush(swapBall);

        // Deselect ball
        setBallState(swapBall, false);

        // Animate ball
        animationManager.animateBall(
            newBottle.peekBall(),
            newBottle,
            levelFactory.ballCount,
            newBottle.getBallQty() - 1);

        // Record action
        if (recordAction)
            actionsManager.pushAction(oldBottle, newBottle);

        // Verify bottle sorting order
        verifyBottle(newBottle);
    }

    public void cancelBottleSelection()
    {
        // Used when a ball is deselected but doesnt change bottles

        Ball selectedBall = selectedBottle?.peekBall();
        if (selectedBall)
        {
            setBallState(selectedBall, false);
            animationManager.animateBall(
                selectedBall, 
                selectedBottle, 
                selectedBottle.getBallQty() - 1);
        }

        selectedBottle = null;
    }

    private void selectBottle(Bottle newBottle)
    {
        // Change selected bottle
        selectedBottle = newBottle;

        // Outline ball
        Ball selectedBall = selectedBottle.peekBall();
        setBallState(selectedBall, true);

        // Animate ball
        animationManager.animateBall(
            selectedBall, 
            selectedBottle, 
            levelFactory.ballCount);
    }
    
    private void verifyBottle(Bottle bottle)
    {
        // Verify if bottle is sorted correctly
        if (bottle.verifyIDs())
        {
            bottle.deactivate();
            bottles.Remove(bottle);
        }

        // 2 is the default empty bottles value
        if (bottles.Count == 2)
            StartCoroutine(showWinDialog());
    }

    private IEnumerator showWinDialog()
    {
        yield return new WaitForSeconds(0.3f);
        winAlert.SetActive(true);
    }

    private void setBallState(Ball ball, bool value)
    {
        // Set outline state
        ball.setActive(value);

        // Play a sound
        if (value)
            audioManager.playSound(AudioManager.Audio.BallActive);
        else
            audioManager.playSound(AudioManager.Audio.BallInactive);
    }
}
