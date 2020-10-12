using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelFactory levelFactory;
    private CameraCentralizer cameraCentralizer;
    private Bottle selectedBottle;
    private Bottle[] bottles;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();
        cameraCentralizer = FindObjectOfType<CameraCentralizer>();
    }

    public void initialize()
    {
        bottles = levelFactory.generateLevel(this, cameraCentralizer);
    }

    public void handleSelection(Bottle newBottle)
    {
        if (selectedBottle)
        {
            if (selectedBottle != newBottle && newBottle.tryPush(selectedBottle.peekBall()))
            {
                selectedBottle.peekBall().setActive(false);
                selectedBottle.popBall();

                verifyBottles();
            }

            deselectBottle();
        }
        else if (newBottle.peekBall())
            selectBottle(newBottle);
    }

    private void selectBottle(Bottle newBottle)
    {
        selectedBottle = newBottle;
        selectedBottle.peekBall().setActive(true);
    }

    private void deselectBottle()
    {
        selectedBottle.peekBall()?.setActive(false);
        selectedBottle = null;
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
