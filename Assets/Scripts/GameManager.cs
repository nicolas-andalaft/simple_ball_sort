using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelFactory levelFactory;
    private CameraCentralizer cameraCentralizer;
    private ActionsManager actionsManager;
    private Bottle selectedBottle;
    private Bottle[] bottles;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();
        cameraCentralizer = FindObjectOfType<CameraCentralizer>();
    }

    public void initialize(ActionsManager actionsManager)
    {
        bottles = levelFactory.generateLevel(this, cameraCentralizer);
        bottles = levelFactory.generateLevel(this);
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

                actionsManager.pushAction(selectedBottle, newBottle);
                verifyBottles();
            }

            deselectBottle();
        }
        else if (newBottle.peekBall())
            selectBottle(newBottle);
    }

    public void deselectBottle()
    {
        if (!selectedBottle)
            return;

        selectedBottle.peekBall()?.setActive(false);
        selectedBottle = null;
    }

    private void selectBottle(Bottle newBottle)
    {
        selectedBottle = newBottle;
        selectedBottle.peekBall().setActive(true);
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
}
