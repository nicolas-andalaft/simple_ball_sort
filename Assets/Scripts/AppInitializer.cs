using UnityEngine;
using GamePlayerPrefs;

public class AppInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameSettingsManager.checkAllPrefs();
    }
}
