using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelFactory levelFactory;
    private CameraCentralizer cameraCentralizer;
    private ActionsManager actionsManager;
    private AnimationManager animationManager;
    private Bottle selectedBottle;
    private Bottle[] bottles;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();
        cameraCentralizer = FindObjectOfType<CameraCentralizer>();
        animationManager = FindObjectOfType<AnimationManager>();
    }

    public void initialize(ActionsManager actionsManager)
    {
        bottles = levelFactory.generateLevel(this, cameraCentralizer);
        this.actionsManager = actionsManager;
    }

    public void handleSelection(Bottle newBottle)
    {
        if (selectedBottle)
        {
            if (selectedBottle != newBottle && newBottle.tryPush(selectedBottle.peekBall()))
            {
                selectedBottle.peekBall().setActive(false);
                selectedBottle.popBall();

                animateBall(newBottle.peekBall(), newBottle, levelFactory.getBallCount());
                animateBall(newBottle.peekBall(), newBottle, newBottle.getBallQty() - 1);

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
            selectedBall.setActive(false);
            animateBall(selectedBall, selectedBottle, selectedBottle.getBallQty() - 1);
        }

        selectedBottle = null;
    }

    private void selectBottle(Bottle newBottle)
    {
        selectedBottle = newBottle;

        Ball selectedBall = selectedBottle.peekBall();
        selectedBall.setActive(true);

        animateBall(selectedBall, selectedBottle, levelFactory.getBallCount());
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

    private void animateBall(Ball ball, Bottle destinationBottle, float yOffset)
    {
        Vector3 destination = new Vector3(
            destinationBottle.transform.position.x,
            destinationBottle.transform.position.y + yOffset,
            0);

        StartCoroutine(animationManager.animateBall(ball, destination));
    }
}
