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

    public bool tryPush(Ball incomingBall)
    {
        if (!incomingBall || container.Count >= containerCapacity) 
            return false;

        if (container.Count == 0 || container.Peek().getId() == incomingBall.getId())
        {
            forcePush(incomingBall);
            return true;
        }
        else
            return false;
    }

    public void forcePush(Ball incomingBall)
    {
        container.Push(incomingBall);
        incomingBall.attach(transform);

        // Set ball position
        Vector3 position = new Vector3(0, container.Count - 1, 0);
        incomingBall.transform.localPosition = position;
    }

    public Ball peekBall()
    {
        if (container.Count == 0)
            return null;
        else
            return container.Peek();
    }

    public void popBall()
    {
        container.Pop();
    }

    private void OnMouseUp()
    {
        GameManager.handleSelection(this);
    }
}
