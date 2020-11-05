using UnityEngine;
using UnityEngine.UI;
using GameKeys;

public class AudioManager : MonoBehaviour
{
    public static bool isActive;
    public enum Audio { BallActive, BallInactive }
    [SerializeField] private AudioSource actionAudioSource;
    [SerializeField] private AudioSource eventAudioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void Awake()
    {
        isActive = (bool)KeyManager.getKey(Keys.Volume);
    }

    public void playSound(Audio audio)
    {
        if (isActive)
        {
            actionAudioSource.clip = audioClips[(int)audio];
            actionAudioSource.Play();
        }
    }

    public void setVolumePrefs(Toggle toggle)
    {
        isActive = toggle.isOn;
        KeyManager.setKey(Keys.Volume, isActive);
    }
}
