using UnityEngine;
using UnityEngine.UI;
using GamePlayerPrefs;

public class HapticFeedback : MonoBehaviour
{
    public static bool isActive;
    private AndroidJavaObject vibrator;

    private void Awake()
    {
        // Get android vibrator
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif

        // Checks player prefs
        isActive = (bool)GameSettingsManager.getPrefs(Prefs.Vibration);
    }

    public void vibrate(long duration)
    {
        if (vibrator != null && isActive)
            vibrator.Call("vibrate", duration);
    }

    public void setVibrationPrefs(Toggle toggle)
    {
        isActive = toggle.isOn;
        GameSettingsManager.setPrefs(Prefs.Vibration, isActive);
    }

    private void handheldCall()
    {
        Handheld.Vibrate();
    }
}
