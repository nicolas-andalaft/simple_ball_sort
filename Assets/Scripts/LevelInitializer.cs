using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();    
    }

    private void Start()
    {
        gameManager.initialize();
    }
}
