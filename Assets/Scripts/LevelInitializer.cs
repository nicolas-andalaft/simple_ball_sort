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

        Bottle[] bottles = levelFactory.generateLevel();
        cameraCentralizer.centralize(levelFactory);

        gameManager.initialize(bottles);
        actionsManager.initialize();
    }
}
