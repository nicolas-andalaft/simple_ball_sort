using UnityEngine;
using UnityEngine.UI;

public class ThemePanel : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Text title;

    public Transform getContentSlot() { return content; }
    public void setTitle(string text) { title.text = text; }
}
