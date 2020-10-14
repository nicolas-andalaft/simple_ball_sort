using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    private AndroidJavaObject vibrator;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    }

    public void vibrate(long duration)
    {
        if (vibrator != null)
            vibrator.Call("vibrate", duration);
        else
            Handheld.Vibrate();
    }
}
