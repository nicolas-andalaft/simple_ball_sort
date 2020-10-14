using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    private bool busy = false;

    public IEnumerator animateBall(Transform ball, Vector3 destination)
    {
        while(busy) 
        {
            yield return new WaitForFixedUpdate(); 
        }

        busy = true;
        while (ball.position.x != destination.x || ball.position.y != destination.y)
        {
            ball.position = Vector3.MoveTowards(ball.position, destination, animationSpeed);
            yield return new WaitForFixedUpdate();
        }
        busy = false;
    }
}
