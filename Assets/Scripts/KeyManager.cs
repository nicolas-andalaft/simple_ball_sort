using System;
using UnityEngine;

namespace GameKeys
{
    public enum Keys 
    { 
        // String type
        Balls       = 0, 
        Bottles     = 1,
        
        // Int type
        BallTypes   = 100, 
        BallCount   = 101, 
        LevelSeed   = 102, 

        // Bool type
        Volume      = 200, 
        Vibration   = 201 
    }

    public class KeyManager : MonoBehaviour
    {
        public static void saveKeys()
        {
            PlayerPrefs.Save();
        }

        public static object getKey(Keys key)
        {
            // String options
            if ((int)key < 100)
                return PlayerPrefs.GetString(key.ToString());

            // Int options
            if ((int)key < 200)
                return PlayerPrefs.GetInt(key.ToString());

            // "Bool" options
            if ((int)key < 300)
            {
                int value = PlayerPrefs.GetInt(key.ToString());
                return value == 1 ? true : false;
            }

            else return null;
        }

        public static void setKey(Keys key, object value)
        {
            // String options
            if ((int)key < 100)
                PlayerPrefs.SetString(key.ToString(), (string)value);

            // Int options
            else if ((int)key < 200)
                PlayerPrefs.SetInt(key.ToString(), (int)value);

            // "Bool" options
            else if ((int)key < 300)
                PlayerPrefs.SetInt(key.ToString(), (bool)value ? 1 : 0);
        }

        public static bool checkKey(Keys key, object defaultValue = null)
        {
            bool hasKey = PlayerPrefs.HasKey(key.ToString());

            if (!hasKey && defaultValue != null)
                setKey(key, defaultValue);

            return hasKey;
        }

        public static void checkAllKeys()
        {
            var keys = Enum.GetValues(typeof(Keys));
            foreach (Keys key in keys)
            {
                checkKey(key, getDeafultKeyValue(key));
            }
        }

        public static void resetKey(Keys key)
        {
            PlayerPrefs.DeleteKey(key.ToString());
            setKey(key, getDeafultKeyValue(key));
        }

        public static void resetAllKeys()
        {
            PlayerPrefs.DeleteAll();
            checkAllKeys();
        }

        public void _saveKeys() { saveKeys(); }
        public void _resetAllKeys() { resetAllKeys(); }

        private static object getDeafultKeyValue(Keys key)
        {
            if (key == Keys.Balls) return Keys.Balls + "_0";
            if (key == Keys.Bottles) return Keys.Bottles + "_0";

            if (key == Keys.BallTypes) return 3;
            if (key == Keys.BallCount) return 4;
            if (key == Keys.LevelSeed) return 0;

            if (key == Keys.Volume) return true;
            if (key == Keys.Vibration) return true;

            else return null;
        }
    }
}
