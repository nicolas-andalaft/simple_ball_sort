using UnityEngine;
using GamePlayerPrefs;

public class AppInitializer : MonoBehaviour
{
    private void Awake()
    {
        checkKeys();
    }

    private void checkKeys()
    {
        // Set default values

        GameSettingsManager.checkPref(Prefs.Balls, Prefs.Balls + "_0");
        GameSettingsManager.checkPref(Prefs.Bottles, Prefs.Bottles + "_0");

        GameSettingsManager.checkPref(Prefs.Volume, 1);
        GameSettingsManager.checkPref(Prefs.Vibration, 1);

        GameSettingsManager.checkPref(Prefs.BallTypes, 3);
        GameSettingsManager.checkPref(Prefs.BallQty, 4);
    }
}
