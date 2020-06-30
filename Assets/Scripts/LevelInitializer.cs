using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    private LevelFactory levelFactory;
    private GameManager gameManager;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();    
        gameManager = FindObjectOfType<GameManager>();    
    }

    private void Start()
    {
        levelFactory.generateLevel(gameManager);
    }
}
