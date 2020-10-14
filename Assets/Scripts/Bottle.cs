using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bottle : MonoBehaviour
{
    public static int containerCapacity { get; set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particles;
    private Stack<Ball> container;

    public void initialize(Sprite sprite)
    {
        // Deactivate particles gameobject
        particles.SetActive(false);

        // Alocate ball container
        if (container == null) 
            container = new Stack<Ball>(containerCapacity);

        // Set collider size and position
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(1.3f, containerCapacity + 0.5f);
        collider.offset = new Vector2(0, (containerCapacity + 0.5f) / 2 - 0.15f);

        // Set bottle sprite
        spriteRenderer.sprite = sprite;
        spriteRenderer.size = new Vector2(1, containerCapacity + 1);
    }

    public int getBallQty()
    {
        return container.Count;
    }

    public bool canPush(Ball incomingBall)
    {
        if (!incomingBall || container.Count >= containerCapacity) 
            return false;

        if (container.Count == 0 || container.Peek().getId() == incomingBall.getId())
            return true;
        else
            return false;
    }

    public void forcePush(Ball incomingBall, bool forcePosition = false)
    {
        container.Push(incomingBall);
        incomingBall.attach(transform);

        // Set ball position
        if (forcePosition)
        {
            Vector3 position = new Vector3(0, container.Count - 1, 0);
            incomingBall.transform.localPosition = position;
        }
    }

    public Ball peekBall()
    {
        if (container.Count == 0)
            return null;
        else
            return container.Peek();
    }

    public Ball popBall()
    {
        return container.Pop();
    }

    public bool verifyIDs()
    {
        if (container.Count == 0)
            return true;
        if (container.Count != containerCapacity)
            return false;

        int firstID = container.ElementAt(0).getId();
        for (int i = 1; i < containerCapacity; i++)
        {
            if (container.ElementAt(i).getId() != firstID)
                return false;
        }
        
        particles.SetActive(true);
        return true;
    }

    public void deactivate()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        GameManager.singleton.handleSelection(this);
    }
}
