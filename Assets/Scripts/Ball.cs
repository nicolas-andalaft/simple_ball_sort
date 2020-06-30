using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Ball : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private SpriteRenderer testColor;
    [SerializeField] private GameObject testOutline;

    public void initialize(int _id, Color _testColor) 
    { 
        id = _id;
        testColor.color = _testColor;
        setActive(false);
    }

    public int getId() { return id; }

    public void attach(Transform parent) { transform.SetParent(parent, true); }

    public void setActive(bool value) { testOutline.SetActive(value); }

}
