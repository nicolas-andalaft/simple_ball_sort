﻿using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [Header("Values")]
    public int ballTypes;
    public int ballCount;
    public int bottleRowQty;
    public float bottleMargin;
    [Header("Prefabs")]
    [SerializeField] private GameObject bottlePrefab = null;
    [SerializeField] private GameObject ballPrefab = null;

    public Bottle[] generateLevel(System.Random randomizer)
    {
        Bottle.containerCapacity = ballCount;

        Bottle[] bottles = instantiateBottles();
        Ball[] ballList = createBallList();

        shuffleBalls(ref ballList, randomizer);
        populateBottles(ref bottles, ballList);

        return bottles;
    }

    private Bottle[] instantiateBottles()
    {
        string resourceName = PlayerPrefs.GetString("Bottles");
        Sprite bottleSprite = Resources.Load<Sprite>(resourceName);

        Bottle[] bottles = new Bottle[ballTypes + 2];

        // Populate array with new Bottles
        for (int i = 0; i < bottles.Length; i++)
        {
            GameObject bottleObj = Instantiate(bottlePrefab);

            float xPosi = i % bottleRowQty * bottleMargin;
            float yPosi = (i / bottleRowQty) * -(ballCount + 1);

            bottleObj.transform.position = new Vector3(xPosi, yPosi, 0);

            Bottle newBottle = bottleObj.GetComponent<Bottle>();
            newBottle.initialize(bottleSprite);
            bottles[i] = newBottle;
        }

        return bottles;
    }

    private Ball[] createBallList()
    {
        string resourceName = PlayerPrefs.GetString("Balls");
        Sprite[] spriteList = Resources.LoadAll<Sprite>(resourceName);

        Ball[] ballList = new Ball[ballCount * ballTypes];

        // Populate array with balls in order
        for (int i = 0; i < ballTypes; i++)
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

    private void shuffleBalls(ref Ball[] ballList, System.Random randomizer)
    {
        int randomIndex;
        for (int i = ballList.Length - 1; i >= 0; i--)
        {
            randomIndex = randomizer.Next(0, ballList.Length);
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
        for (int i = 0; i < ballTypes; i++)
        {
            for (int j = 0; j < ballCount; j++)
                bottles[i].forcePush(ballList[i * ballCount + j], true);
        }
    }
}
