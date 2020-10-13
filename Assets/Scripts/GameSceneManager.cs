using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void loadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void loadPlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
