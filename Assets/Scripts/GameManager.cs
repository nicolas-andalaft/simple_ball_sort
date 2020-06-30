using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager GM; //singleton
    private Bottle selectedBottle;

    public void initialize()
    {
        if (!GM)
            GM = this;
    }

    public static void handleSelection(Bottle newBottle) { GM._handleSelection(newBottle); }
    private void _handleSelection(Bottle newBottle)
    {
        if (selectedBottle)
        {
            if (selectedBottle != newBottle && newBottle.tryPush(selectedBottle.peekBall()))
            {
                selectedBottle.peekBall().setActive(false);
                selectedBottle.popBall();
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
}
