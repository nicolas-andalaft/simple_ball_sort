using System.Collections.Generic;

public class Bottle
{
    private static int containerCapacity;
    private Stack<Ball> container;

    public Bottle(int ballCount)
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
        }
    }
}
