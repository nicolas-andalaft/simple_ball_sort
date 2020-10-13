using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    private GameManager gameManager;
    private ActionsManager actionsManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        actionsManager = FindObjectOfType<ActionsManager>();
    }

    private void Start()
    {
        gameManager.initialize(actionsManager);
        actionsManager.initialize(gameManager);
    }
}
