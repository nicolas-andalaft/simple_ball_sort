using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlayerPrefs
{
    public enum Prefs { Balls, Bottles, Volume, Vibration, BallTypes, BallCount, LevelSeed }

    public class GameSettingsManager : MonoBehaviour
    {
        [SerializeField] private Toggle volumeToggle;
        [SerializeField] private Toggle vibrationToggle;
        [SerializeField] private SliderManager ballTypesSlider;
        [SerializeField] private SliderManager ballQtySlider;

        public static object getPrefs(Prefs pref)
        {
            switch (pref)
            {
                // String options
                case Prefs.Balls:
                case Prefs.Bottles:
                    return PlayerPrefs.GetString(pref.ToString());

                // "Bool" options
                case Prefs.Volume:
                case Prefs.Vibration:
                    int value = PlayerPrefs.GetInt(pref.ToString());
                    return value == 1 ? true : false;

                // Int options
                case Prefs.BallTypes:
                case Prefs.BallCount:
                case Prefs.LevelSeed:
                    return PlayerPrefs.GetInt(pref.ToString());

                default:
                    return null;
            }
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
            if (pref == Prefs.BallTypes || pref == Prefs.BallCount || pref == Prefs.LevelSeed)
                PlayerPrefs.SetInt(pref.ToString(), (int)value);
        }

        public static bool checkPref(Prefs pref, object defaultValue = null)
        {
            bool hasKey = PlayerPrefs.HasKey(pref.ToString());

            if (!hasKey && defaultValue != null)
                setPrefs(pref, defaultValue);

            return hasKey;
        }

        public static void checkAllPrefs()
        {
            checkPref(Prefs.Balls, Prefs.Balls + "_0");
            checkPref(Prefs.Bottles, Prefs.Bottles + "_0");

            checkPref(Prefs.Volume, true);
            checkPref(Prefs.Vibration, true);

            checkPref(Prefs.BallTypes, 3);
            checkPref(Prefs.BallCount, 4);
            checkPref(Prefs.LevelSeed, 0);
        }

        public void deleteAllPrefs()
        {
            PlayerPrefs.DeleteAll();
            checkAllPrefs();
            savePlayerPrefs();

            updateToggles();
            updateSliders();
        }

        public static void deletePref(Prefs pref)
        {
            PlayerPrefs.DeleteKey(pref.ToString());
        }

        public void savePlayerPrefs()
        {
            PlayerPrefs.Save();
        }

        public void updateToggles()
        {
            volumeToggle.isOn = (bool)getPrefs(Prefs.Volume);
            vibrationToggle.isOn = (bool)getPrefs(Prefs.Vibration);
        }

        public void updateSliders()
        {
            ballTypesSlider.updateIndicator((int)getPrefs(Prefs.BallTypes));
            ballQtySlider.updateIndicator((int)getPrefs(Prefs.BallCount));
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
