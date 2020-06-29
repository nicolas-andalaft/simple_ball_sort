using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    private LevelFactory levelFactory;

    private void Awake()
    {
        levelFactory = FindObjectOfType<LevelFactory>();    
    }

    private void Start()
    {
        levelFactory.generateLevel();
    }
}
