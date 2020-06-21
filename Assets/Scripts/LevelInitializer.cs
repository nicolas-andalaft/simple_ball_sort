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

        for (int i = 0; i < ballCount; i++)
            bottles[i] = new Bottle(ballCount);
    }

    private void Start()
    {
        foreach (Bottle bottle in bottles)
            randomizeBalls(bottle);
    }

    private void randomizeBalls(Bottle bottle)
    {
        for (int i = 0; i < 4; i++)
        {
            int randomID = Random.Range(0, 10);
            Ball newBall = new Ball(randomID);
            bottle.tryPush(newBall);

            print("Ball " + i + ": " + randomID);
        }
    }
}
