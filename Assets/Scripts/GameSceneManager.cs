using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void loadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void loadThemesMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void loadPlayScene()
    {
        SceneManager.LoadScene(2);
    }
}
