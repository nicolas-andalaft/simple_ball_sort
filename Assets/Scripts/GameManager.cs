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
            if (selectedBottle != newBottle && newBottle.tryPush(selectedBottle.peekBall()))
            {
                setBallState(selectedBottle.peekBall(), false);
                selectedBottle.popBall();

                StartCoroutine(animationManager.animateBall(
                    newBottle.peekBall(), 
                    newBottle, 
                    levelFactory.getBallCount(), 
                    newBottle.getBallQty() - 1));

                actionsManager.pushAction(selectedBottle, newBottle);
                verifyBottle(newBottle);
                selectedBottle = null;
            }
            else
                deselectBottle();
        }
        else if (newBottle.peekBall())
            selectBottle(newBottle);
    }

    public void deselectBottle()
    {
        if (!selectedBottle)
            return;

        Ball selectedBall = selectedBottle.peekBall();
        if (selectedBall)
        {
            setBallState(selectedBall, false);
            StartCoroutine(animationManager.animateBall(
                selectedBall, 
                selectedBottle, 
                selectedBottle.getBallQty() - 1));
        }

        selectedBottle = null;
    }

    private void selectBottle(Bottle newBottle)
    {
        selectedBottle = newBottle;

        Ball selectedBall = selectedBottle.peekBall();
        setBallState(selectedBall, true);

        StartCoroutine(animationManager.animateBall(
            selectedBall, 
            selectedBottle, 
            levelFactory.getBallCount()));
    }
    
    private void verifyBottle(Bottle bottle)
    {
        if (bottle.verifyIDs())
        {
            bottle.deactivate();
            bottles.Remove(bottle);
        }

        if (bottles.Count == 2)
            print("Win");
    }

    private void setBallState(Ball ball, bool value)
    {
        ball.setActive(value);
        if (value)
            audioManager.playSound(AudioManager.Audio.BallActive);
        else
            audioManager.playSound(AudioManager.Audio.BallInactive);
    }
}
