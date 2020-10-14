using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelFactory levelFactory;
    private CameraCentralizer cameraCentralizer;
    private ActionsManager actionsManager;
    private AnimationManager animationManager;
    private AudioManager audioManager;
    private HapticFeedback hapticFeedback;
    private Bottle selectedBottle;
    private Bottle[] bottles;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();
        cameraCentralizer = FindObjectOfType<CameraCentralizer>();
        animationManager = FindObjectOfType<AnimationManager>();
        audioManager = FindObjectOfType<AudioManager>();
        hapticFeedback = FindObjectOfType<HapticFeedback>();
    }

    public void initialize(ActionsManager actionsManager)
    {
        bottles = levelFactory.generateLevel(this, cameraCentralizer);
        this.actionsManager = actionsManager;
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
                selectedBottle = null;
                verifyBottles();
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
    
    private void verifyBottles()
    {
        bool correct = true;
        for (int i = 0; correct && i < bottles.Length; i++)
        {
            if (!bottles[i].verifyIDs())
                correct = false;
        }

        if (correct)
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
