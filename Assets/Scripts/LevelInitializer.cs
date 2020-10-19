using UnityEngine;
using GamePlayerPrefs;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ActionsManager actionsManager;
    [SerializeField] private LevelFactory levelFactory;
    [SerializeField] private CameraCentralizer cameraCentralizer;

    private void Start()
    {
        levelFactory.ballTypes = (int)GameSettingsManager.getPrefs(Prefs.BallTypes);
        levelFactory.ballCount = (int)GameSettingsManager.getPrefs(Prefs.BallCount);

        Bottle[] bottles = levelFactory.generateLevel(getLevelRandomizer());
        cameraCentralizer.centralize(levelFactory);

        gameManager.initialize(bottles);
        actionsManager.initialize();
    }

    private System.Random getLevelRandomizer()
    {
        System.Random randomizer;
        int seed = (int)GameSettingsManager.getPrefs(Prefs.LevelSeed);

        if (seed != 0)
            randomizer = new System.Random(seed);
        else
        {
            seed = Random.Range(0, int.MaxValue);
            randomizer = new System.Random(seed);

            GameSettingsManager.setPrefs(Prefs.LevelSeed, seed);
            PlayerPrefs.Save();
        }

        return randomizer;
    }
}
