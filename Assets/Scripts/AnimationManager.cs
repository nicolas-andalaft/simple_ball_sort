using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    private bool busy = false;

    public IEnumerator animateBall(Ball ball, Vector3 destination)
    {
        while(busy) 
        {
            yield return new WaitForFixedUpdate(); 
        }

        busy = true;
        while (ball.transform.position.x != destination.x || ball.transform.position.y != destination.y)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, destination, animationSpeed);
            yield return new WaitForFixedUpdate();
        }

        ball.animate();
        busy = false;
    }
}
