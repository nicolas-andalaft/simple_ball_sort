using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ActionsManager actionsManager;
    [SerializeField] private LevelFactory levelFactory;
    [SerializeField] private CameraCentralizer cameraCentralizer;

    private void Start()
    {
        Bottle[] bottles = levelFactory.generateLevel();
        cameraCentralizer.centralize(levelFactory);

        gameManager.initialize(bottles);
        actionsManager.initialize();
    }
}
