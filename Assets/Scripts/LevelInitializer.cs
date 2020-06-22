using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private int bottlesQty;
    [SerializeField] private int ballCount;
    private Bottle[] bottles;

    private void Awake()
    {
        // Instanciate bottles array with Unity Inspector value
        bottles = new Bottle[bottlesQty];

        // Populate array with new Bottles
        for (int i = 0; i < bottlesQty; i++)
            bottles[i] = new Bottle(ballCount);
    }

    private void Start()
    {
        Ball[] ballList = createBallList();
        shuffleBalls(ref ballList);
        populateBottles(ballList);
    }

    private Ball[] createBallList()
    {
        Ball[] ballList = new Ball[ballCount * bottlesQty];

        // Populate array with balls in order
        for (int i = 0; i < bottlesQty; i++)
        {
            for (int j = 0; j < ballCount; j++)
            {
                Ball newBall = new Ball(i);
                ballList[i * ballCount + j] = newBall;
            }
        }

        return ballList;
    }

    private void shuffleBalls(ref Ball[] ballList)
    {
        int randomIndex;
        for (int i = ballList.Length -1; i >= 0; i--)
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

    private void populateBottles(Ball[] ballList)
    {
        for (int i = 0; i < bottlesQty; i++)
        {
            for (int j = 0; j < ballCount; j++)
                bottles[i].tryPush(ballList[i * ballCount + j]);
        }
    }


}
