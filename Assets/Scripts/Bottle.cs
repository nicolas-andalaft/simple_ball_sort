using System.Collections.Generic;

public class Bottle
{
    private Stack<Ball> container;

    public Bottle(int ballCount)
    {
        container = new Stack<Ball>(ballCount);
    }

    public void tryPush(Ball incomingBall)
    {
        if (container.Count >= 4) return;
        if (container.Count == 0 || container.Peek().getId() == incomingBall.getId())
        {
            container.Push(incomingBall);
        }
    }
}
