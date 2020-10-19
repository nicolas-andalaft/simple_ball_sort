using UnityEngine;
using GameKeys;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ActionsManager actionsManager;
    [SerializeField] private LevelFactory levelFactory;
    [SerializeField] private CameraCentralizer cameraCentralizer;

    private void Start()
    {
        levelFactory.ballTypes = (int)KeyManager.getKey(Keys.BallTypes);
        levelFactory.ballCount = (int)KeyManager.getKey(Keys.BallCount);

        Bottle[] bottles = levelFactory.generateLevel(getLevelRandomizer());
        cameraCentralizer.centralize(levelFactory);

        gameManager.initialize(bottles);
        actionsManager.initialize();
    }

    private System.Random getLevelRandomizer()
    {
        System.Random randomizer;
        int seed = (int)KeyManager.getKey(Keys.LevelSeed);

        if (seed != 0)
            randomizer = new System.Random(seed);
        else
        {
            seed = Random.Range(0, int.MaxValue);
            randomizer = new System.Random(seed);

            KeyManager.setKey(Keys.LevelSeed, seed);
            PlayerPrefs.Save();
        }

        return randomizer;
    }
}
