using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;

    public void animateBall(Ball ball, Bottle destinationBottle, params float[] positions)
    {
        StartCoroutine(animate(ball, destinationBottle, positions));
    }

    public IEnumerator animate(Ball ball, Bottle destinationBottle, params float[] positions)
    {
        foreach (float offset in positions)
        {
            Vector3 destination = new Vector3(
                destinationBottle.transform.position.x,
                destinationBottle.transform.position.y + offset,
                0);

            while (ball.transform.position.x != destination.x || ball.transform.position.y != destination.y)
            {
                ball.transform.position = Vector3.MoveTowards(ball.transform.position, destination, animationSpeed);
                yield return new WaitForFixedUpdate();
            }
        }

        ball.animate();
    }
}
