using UnityEngine;
using UnityEngine.UI;
using GameKeys;

public class ThemesManager : MonoBehaviour
{
    public static Color _activeColor { get; private set; }
    public static Color _inactiveColor { get; private set; }
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private RectTransform themePanelsSlot;
    [SerializeField] private GameObject themePanel;
    [SerializeField] private GameObject themeContainer;
    [SerializeField] private GameObject themeItem;

    private void Start()
    {
        _activeColor = activeColor;
        _inactiveColor = inactiveColor;
        instantiateMenu();
    }

    private void instantiateMenu()
    {
        instantiatePanel(Keys.Balls);
        instantiatePanel(Keys.Bottles);
    }

    private void instantiatePanel(Keys resource)
    {
        // Instantiate theme panel
        GameObject panel = Instantiate(themePanel, themePanelsSlot);

        ThemePanel themePanelScript = panel.GetComponent<ThemePanel>();
        themePanelScript.setTitle(resource.ToString());

        Transform slot = themePanelScript.getContentSlot();

        // Get last child
        while (slot.childCount != 0)
            slot = slot.GetChild(0);

        // Sequencial search for resources
        int i = 0;
        Sprite[] sprites = Resources.LoadAll<Sprite>(resource + "_" + i);

        while (sprites.Length > 0)
        {
            var themeContainerScript = instantiateThemeContainer(slot, sprites);
            themeContainerScript.setThemePanel(themePanelScript);
            themeContainerScript.setResource(resource);
            themeContainerScript.setPackName(resource + "_" + i);

            if (KeyManager.getKey(resource) == (resource + "_" + i))
                themeContainerScript.setPack();

            i++;
            sprites = Resources.LoadAll<Sprite>(resource + "_" + i);
        }
    }

    private ThemeContainer instantiateThemeContainer(Transform parent, Sprite[] sprites)
    {
        // Instantiate theme container
        Transform slot = Instantiate(themeContainer, parent).transform;
        var themeContainerScript = slot.GetComponent<ThemeContainer>();

        // Get last child
        while (slot.childCount != 0)
            slot = slot.GetChild(0);

        // Checks if resource has multiple sprites
        if (sprites.Length == 1)
            slot.GetComponent<GridLayoutGroup>().enabled = false;

        // Instantitate theme items
        for (int i = 0; i < sprites.Length; i++)
            instantitateThemeItem(slot, sprites[i]);

        return themeContainerScript;
    }

    private void instantitateThemeItem(Transform parent, Sprite sprite)
    {
        GameObject item = Instantiate(themeItem, parent);

        item.GetComponent<Image>().sprite = sprite;
    }
}
