using UnityEngine;
using GameKeys;
using UnityEngine.UI;

public class AppInitializer : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private Toggle volumeToggle;
    [SerializeField] private Toggle vibrationToggle;
    [Header("Level Settings")]
    [SerializeField] private SliderManager ballTypesSlider;
    [SerializeField] private SliderManager ballCountSlider;

    private void Awake()
    {
        KeyManager.checkAllKeys();
    }

    public void updateToggles()
    {
        volumeToggle.isOn = (bool)KeyManager.getKey(Keys.Volume);
        vibrationToggle.isOn = (bool)KeyManager.getKey(Keys.Vibration);
    }

    public void updateSliders()
    {
        ballTypesSlider.updateIndicator((int)KeyManager.getKey(Keys.BallTypes));
        ballCountSlider.updateIndicator((int)KeyManager.getKey(Keys.BallCount));
    }
}
