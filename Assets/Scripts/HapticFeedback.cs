using UnityEngine;
using UnityEngine.UI;

public class HapticFeedback : MonoBehaviour
{
    private static bool isActive;
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
        if (PlayerPrefs.GetInt("Vibration") == 0)
            isActive = false;
        else
            isActive = true;
    }

    public void vibrate(long duration)
    {
        if (vibrator != null && isActive)
            vibrator.Call("vibrate", duration);
    }

    public void setVibrationPrefs(Toggle toggle)
    {
        PlayerPrefs.SetInt("Vibration", toggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void handheldCall()
    {
        Handheld.Vibrate();
    }
}
