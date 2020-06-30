using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int bottlesQty;
    [SerializeField] private int ballCount;
    [SerializeField] private float bottleMargin;
    [Header("Prefabs")]
    [SerializeField] private GameObject bottlePrefab = null;
    [SerializeField] private GameObject ballPrefab = null;

    private Bottle[] bottles;

    public void generateLevel(GameManager gameManager)
    {
        instantiateBottles(gameManager);
        Ball[] ballList = createBallList();
        shuffleBalls(ref ballList);
        populateBottles(ballList);
    }

    private void instantiateBottles(GameManager gameManager)
    {
        bottles = new Bottle[bottlesQty + 2];

        // Populate array with new Bottles
        for (int i = 0; i < bottles.Length; i++)
        {
            GameObject bottleObj = Instantiate(bottlePrefab);
            bottleObj.transform.position = new Vector3(i * bottleMargin, 0, 0);

            Bottle newBottle = bottleObj.GetComponent<Bottle>();
            newBottle.initialize(ballCount, gameManager);
            bottles[i] = newBottle;
        }
    }

    private Ball[] createBallList()
    {
        Ball[] ballList = new Ball[ballCount * bottlesQty];

        // Populate array with balls in order
        for (int i = 0; i < bottlesQty; i++)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            for (int j = 0; j < ballCount; j++)
            {
                Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
                newBall.initialize(i, randomColor);
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

    private void populateBottles(Ball[] ballList)
    {
        for (int i = 0; i < bottlesQty; i++)
        {
            for (int j = 0; j < ballCount; j++)
                bottles[i].forcePush(ballList[i * ballCount + j]);
        }
    }
}
