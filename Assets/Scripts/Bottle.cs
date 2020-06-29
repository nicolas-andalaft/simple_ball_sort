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
        if (container.Count >= containerCapacity) return;
        if (container.Count == 0 || container.Peek().getId() == incomingBall.getId())
        {
            container.Push(incomingBall);
            incomingBall.attach(transform);
        }
    }

    public void forcePush(Ball incomingBall)
    {
        container.Push(incomingBall);
        incomingBall.attach(transform);
    }
}
