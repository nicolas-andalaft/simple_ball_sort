using GameKeys;
using System;
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

    public void setKeyValue(string keyName)
    {
        try
        {
            var key = Enum.Parse(typeof(Keys), keyName);
            KeyManager.setKey((Keys)key, (int)slider.value);
        }
        catch { }
    }
}
