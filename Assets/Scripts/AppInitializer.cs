using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    private void Awake()
    {
        checkKeys();
    }

    private void checkKeys()
    {
        bool changed = false;

        if (!PlayerPrefs.HasKey("Balls"))
        {
            changed = true;
            PlayerPrefs.SetString("Balls", "Balls_0");
        }

        if (!PlayerPrefs.HasKey("Bottles"))
        {
            changed = true;
            PlayerPrefs.SetString("Bottles", "Bottles_0");
        }

        if (changed)
            PlayerPrefs.Save();
    }
}
