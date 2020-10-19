using UnityEngine;
using UnityEngine.UI;

namespace GamePlayerPrefs
{
    public enum Prefs { Balls, Bottles, Volume, Vibration }

    public class GameSettingsManager : MonoBehaviour
    {
        [SerializeField] private Toggle volumeToggle;
        [SerializeField] private Toggle vibrationToggle;

        public static object getPrefs(Prefs pref)
        {
            // String options
            if (pref == Prefs.Balls || pref == Prefs.Bottles)
                return PlayerPrefs.GetString(pref.ToString());

            // Int options
            if (pref == Prefs.Volume || pref == Prefs.Vibration)
            {
                int value = PlayerPrefs.GetInt(pref.ToString());
                return value == 1 ? true : false;
            }

            // Else
            return null;
        }

        public static void setPrefs(Prefs pref, object value)
        {
            // String options
            if (pref == Prefs.Balls || pref == Prefs.Bottles)
                PlayerPrefs.SetString(pref.ToString(), (string)value);

            // Int options
            if (pref == Prefs.Volume || pref == Prefs.Vibration)
                PlayerPrefs.SetInt(pref.ToString(), (bool)value ? 1 : 0);
        }

        public void updateToggles()
        {
            volumeToggle.isOn = AudioManager.isActive;
            vibrationToggle.isOn = HapticFeedback.isActive;
        }

        public void savePlayerPrefs()
        {
            PlayerPrefs.Save();
        }
    }
}
