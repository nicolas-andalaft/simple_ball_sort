using UnityEngine;
using UnityEngine.UI;

public class ThemeContainer : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private string resourceName;
    [SerializeField] private string packName;
    [SerializeField] private ThemePanel panelParent;

    public Button getButton() { return button; }

    public void setResourceName(string value) { resourceName = value; }
    public void setPackName(string value) { packName = value; }
    public void setThemePanel(ThemePanel themePanel) { panelParent = themePanel; }

    public void setPack()
    {
        image.color = ThemesManager._activeColor;

        if (panelParent.selectedPackImage)
            panelParent.selectedPackImage.color = ThemesManager._inactiveColor;
        panelParent.selectedPackImage = image;

        PlayerPrefs.SetString(resourceName, packName);
    }
}
