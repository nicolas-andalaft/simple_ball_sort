using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bottle selectedBottle;

    public void handleSelection(Bottle newBottle)
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
