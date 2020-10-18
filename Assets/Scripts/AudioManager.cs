using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static bool isActive;
    public enum Audio { BallActive, BallInactive }
    [SerializeField] private AudioSource actionAudioSource;
    [SerializeField] private AudioSource eventAudioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("Volume") == 0)
            isActive = false;
        else
            isActive = true;
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
        PlayerPrefs.SetInt("Volume", toggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
