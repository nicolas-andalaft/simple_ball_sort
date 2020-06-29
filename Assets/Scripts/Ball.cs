using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Ball : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private SpriteRenderer testColor;

    public void initialize(int _id, Color _testColor) 
    { 
        id = _id;
        testColor.color = _testColor;
    }

    public int getId() { return id; }

    public void attach(Transform parent) { transform.SetParent(parent, false); }

}
