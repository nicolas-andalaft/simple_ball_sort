﻿using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int bottlesQty;
    [SerializeField] private int ballCount;
    [SerializeField] private float bottleMargin;
    [SerializeField] private int bottleRowQty;
    [Header("Prefabs")]
    [SerializeField] private GameObject bottlePrefab = null;
    [SerializeField] private GameObject ballPrefab = null;

    public int getBottlesQty() { return bottlesQty; }
    public int getBallCount() { return ballCount; }
    public float getBottleMargin() { return bottleMargin; }
    public int getBottleRowQty() { return bottleRowQty; }

    public Bottle[] generateLevel(GameManager gameManager, CameraCentralizer cameraCentralizer)
    {
        Bottle[] bottles = instantiateBottles(gameManager);
        Ball[] ballList = createBallList();

        shuffleBalls(ref ballList);
        populateBottles(ref bottles, ballList);

        float xMax = (bottleRowQty - 1) * bottleMargin;
        float yMax = (bottles.Length - 1) / bottleRowQty * -0.5f;
        cameraCentralizer.centralize(xMax, yMax);

        return bottles;
    }

    private Bottle[] instantiateBottles(GameManager gameManager)
    {
        Sprite bottleSprite = Resources.Load<Sprite>("Bottle_1");
        Bottle[] bottles = new Bottle[bottlesQty + 2];

        // Populate array with new Bottles
        for (int i = 0; i < bottles.Length; i++)
        {
            GameObject bottleObj = Instantiate(bottlePrefab);

            float xPosi = i % bottleRowQty * bottleMargin;
            float yPosi = (i / bottleRowQty) * -(ballCount + 1);

            bottleObj.transform.position = new Vector3(xPosi, yPosi, 0);

            Bottle newBottle = bottleObj.GetComponent<Bottle>();
            newBottle.initialize(ballCount, gameManager);
            newBottle.updateBottle(bottleSprite);
            bottles[i] = newBottle;
        }

        return bottles;
    }

    private Ball[] createBallList()
    {
        Ball[] ballList = new Ball[ballCount * bottlesQty];
        Sprite[] spriteList = Resources.LoadAll<Sprite>("Ball_1");

        // Populate array with balls in order
        for (int i = 0; i < bottlesQty; i++)
        {
            for (int j = 0; j < ballCount; j++)
            {
                Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
                newBall.initialize(i, spriteList[i]);
                ballList[i * ballCount + j] = newBall;
            }
        }

        return ballList;
    }

    private void shuffleBalls(ref Ball[] ballList)
    {
        int randomIndex;
        for (int i = ballList.Length - 1; i >= 0; i--)
        {
            randomIndex = Random.Range(0, i);
            if (randomIndex != i)
            {
                Ball aux = ballList[randomIndex];
                ballList[randomIndex] = ballList[i];
                ballList[i] = aux;
            }
        }
    }

    private void populateBottles(ref Bottle[] bottles, Ball[] ballList)
    {
        for (int i = 0; i < bottlesQty; i++)
        {
            for (int j = 0; j < ballCount; j++)
                bottles[i].forcePush(ballList[i * ballCount + j], true);
        }
    }
}
