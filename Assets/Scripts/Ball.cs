using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Ball : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject testOutline;

    public void initialize(int _id, Sprite sprite) 
    { 
        id = _id;
        spriteRenderer.sprite = sprite;
        setActive(false);
    }

    public int getId() { return id; }

    public void attach(Transform parent) { transform.SetParent(parent, true); }

    public void setActive(bool value) { testOutline.SetActive(value); }

}
