using System.Collections.Generic;
using UnityEngine;
using Actions;

public class ActionsManager : MonoBehaviour
{
    [SerializeField] private int maxUndos;
    private GameManager gameManager;
    private List<Action> actions;

    public void initialize()
    {
        actions = new List<Action>(maxUndos);
        gameManager = GameManager.singleton;
    }

    public void pushAction(Bottle poped, Bottle pushed)
    {
        Action newAction = new Action();
        newAction.poped = poped;
        newAction.pushed = pushed;

        pushAction(newAction);
    }

    public void undoAction()
    {
        if (actions.Count == 0)
            return;

        Action undoAction = popAction();

        gameManager.deselectBottle();
        undoAction.poped.forcePush(undoAction.pushed.popBall());
    }

    private void pushAction(Action newAction)
    {
        if (actions.Count == maxUndos)
            actions.RemoveAt(0);

        actions.Add(newAction);
    }

    private Action popAction()
    {
        int last = actions.Count - 1;

        Action popedAction = actions[last];
        actions.RemoveAt(last);

        return popedAction;
    }
}
