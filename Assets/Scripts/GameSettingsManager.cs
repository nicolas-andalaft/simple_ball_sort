using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlayerPrefs
{
    public enum Prefs { Balls, Bottles, Volume, Vibration, BallTypes, BallCount }

    public class GameSettingsManager : MonoBehaviour
    {
        [SerializeField] private Toggle volumeToggle;
        [SerializeField] private Toggle vibrationToggle;
        [SerializeField] private SliderManager ballTypesSlider;
        [SerializeField] private SliderManager ballQtySlider;

        public static object getPrefs(Prefs pref)
        {
            // String options
            if (pref == Prefs.Balls || pref == Prefs.Bottles)
                return PlayerPrefs.GetString(pref.ToString());

            // "Bool" options
            if (pref == Prefs.Volume || pref == Prefs.Vibration)
            {
                int value = PlayerPrefs.GetInt(pref.ToString());
                return value == 1 ? true : false;
            }

            // Int options
            if (pref == Prefs.BallTypes || pref == Prefs.BallCount)
                return PlayerPrefs.GetInt(pref.ToString());

            // Else
            return null;
        }

        public static void setPrefs(Prefs pref, object value)
        {
            // String options
            if (pref == Prefs.Balls || pref == Prefs.Bottles)
                PlayerPrefs.SetString(pref.ToString(), (string)value);

            // "Bool" options
            if (pref == Prefs.Volume || pref == Prefs.Vibration)
                PlayerPrefs.SetInt(pref.ToString(), (bool)value ? 1 : 0);

            // Int options
            if (pref == Prefs.BallTypes || pref == Prefs.BallCount)
                PlayerPrefs.SetInt(pref.ToString(), (int)value);
        }

        public static bool checkPref(Prefs pref, object defaultValue = null)
        {
            bool hasKey = PlayerPrefs.HasKey(pref.ToString());

            if (!hasKey && defaultValue != null)
                setPrefs(pref, defaultValue);

            return hasKey;
        }

        public void updateToggles()
        {
            volumeToggle.isOn = AudioManager.isActive;
            vibrationToggle.isOn = HapticFeedback.isActive;
        }

        public void updateSliders()
        {
            ballTypesSlider.updateIndicator((int)getPrefs(Prefs.BallTypes));
            ballQtySlider.updateIndicator((int)getPrefs(Prefs.BallCount));
        }

        public void savePlayerPrefs()
        {
            PlayerPrefs.Save();
        }

        public void setBallTypesPref(Slider slider)
        {
            setPrefs(Prefs.BallTypes, (int)slider.value);
        }

        public void setBallCountPref(Slider slider)
        {
            setPrefs(Prefs.BallCount, (int)slider.value);
        }
    }
}
