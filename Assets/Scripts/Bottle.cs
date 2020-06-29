using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Bottle : MonoBehaviour
{
    [SerializeField] private static int containerCapacity;
    [SerializeField] private Stack<Ball> container;

    public void initialize(int ballCount)
    {
        containerCapacity = ballCount;
        container = new Stack<Ball>(containerCapacity);
    }

    public void tryPush(Ball incomingBall)
    {
        if (container.Count >= containerCapacity) 
            return;

        if (container.Count == 0 || container.Peek().getId() == incomingBall.getId())
            forcePush(incomingBall);
    }

    public void forcePush(Ball incomingBall)
    {
        container.Push(incomingBall);
        incomingBall.attach(transform);

        // Set ball position
        Vector3 position = new Vector3(0, container.Count - 1, 0);
        incomingBall.transform.localPosition += position;
    }
}
