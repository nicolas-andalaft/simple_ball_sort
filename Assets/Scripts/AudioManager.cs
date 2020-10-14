using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Audio { BallActive, BallInactive }
    [SerializeField] private AudioSource actionAudioSource;
    [SerializeField] private AudioSource eventAudioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void playSound(Audio audio)
    {
        actionAudioSource.clip = audioClips[(int)audio];
        actionAudioSource.Play();
    }
}
