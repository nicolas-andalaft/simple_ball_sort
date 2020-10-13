﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bottle : MonoBehaviour
{
    [SerializeField] private static int containerCapacity;
    [SerializeField] private static GameManager gameManager;
    [SerializeField] private Stack<Ball> container;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    public void initialize(int ballCount, GameManager _gameManager)
    {
        if (containerCapacity == 0) containerCapacity = ballCount;
        if (container == null) container = new Stack<Ball>(containerCapacity);
        if (gameManager == null) gameManager = _gameManager;

        collider = GetComponent<BoxCollider2D>();
    }

    public void updateBottle(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.size = new Vector2(1, containerCapacity + 1);
        collider.size = new Vector2(1.3f, containerCapacity + 0.5f);
        collider.offset = new Vector2(0, (containerCapacity + 0.5f) / 2 - 0.15f);
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

        bool correct = true;
        int firstID = container.ElementAt(0).getId();

        for (int i = 1; correct && i < containerCapacity; i++)
        {
            if (container.ElementAt(i).getId() != firstID)
                correct = false;
        }

        return correct;
    }

    private void OnMouseUp()
    {
        gameManager.handleSelection(this);
    }
}
