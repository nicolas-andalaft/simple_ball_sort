using UnityEngine;
using UnityEngine.UI;
using GamePlayerPrefs;

public class ThemeContainer : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Prefs resource;
    [SerializeField] private string packName;
    [SerializeField] private ThemePanel panelParent;

    public Button getButton() { return button; }

    public void setResource(Prefs pref) { resource = pref; }
    public void setPackName(string value) { packName = value; }
    public void setThemePanel(ThemePanel themePanel) { panelParent = themePanel; }

    public void setPack()
    {
        image.color = ThemesManager._activeColor;

        if (panelParent.selectedPackImage)
            panelParent.selectedPackImage.color = ThemesManager._inactiveColor;
        panelParent.selectedPackImage = image;

        GameSettingsManager.setPrefs(resource, packName);
    }
}
