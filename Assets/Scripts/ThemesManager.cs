using UnityEngine;
using UnityEngine.UI;

public class ThemesManager : MonoBehaviour
{
    [SerializeField] private RectTransform themePanelsSlot;
    [SerializeField] private GameObject themePanel;
    [SerializeField] private GameObject themeContainer;
    [SerializeField] private GameObject themeItem;

    private void Start()
    {
        instantiateMenu();
    }

    private void instantiateMenu()
    {
        instantiatePanel("Balls");
        instantiatePanel("Bottles");
    }

    private void instantiatePanel(string resourceName)
    {
        // Instantiate theme panel
        GameObject panel = Instantiate(themePanel, themePanelsSlot);

        ThemePanel themePanelScript = panel.GetComponent<ThemePanel>();
        themePanelScript.setTitle(resourceName);

        Transform slot = themePanelScript.getContentSlot();

        // Get last child
        while (slot.childCount != 0)
            slot = slot.GetChild(0);

        // Sequencial search for resources
        int i = 0;
        Sprite[] sprites = Resources.LoadAll<Sprite>(resourceName + "_" + i);

        while (sprites.Length > 0)
        {
            instantiateThemeContainer(slot, sprites);

            i++;
            sprites = Resources.LoadAll<Sprite>(resourceName + "_" + i);
        }
    }

    private void instantiateThemeContainer(Transform parent, Sprite[] sprites)
    {
        // Instantiate theme container
        Transform slot = Instantiate(themeContainer, parent).transform;

        // Get last child
        while (slot.childCount != 0)
            slot = slot.GetChild(0);

        // Instantitate theme items
        for (int i = 0; i < sprites.Length; i++)
            instantitateThemeItem(slot, sprites[i]);
    }

    private void instantitateThemeItem(Transform parent, Sprite sprite)
    {
        GameObject item = Instantiate(themeItem, parent);
        item.GetComponent<Image>().sprite = sprite;
    }
}
