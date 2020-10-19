using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Text indicator;
    [SerializeField] private Slider slider;

    public void updateIndicator(float value)
    {
        indicator.text = value.ToString();
        slider.value = value;
    }
}
