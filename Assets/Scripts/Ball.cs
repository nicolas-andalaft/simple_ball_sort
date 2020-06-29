using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Ball : MonoBehaviour
{
    [SerializeField] private int id;

    public void initialize(int _id, Color testColor) 
    { 
        id = _id;
        GetComponent<SpriteRenderer>().color = testColor;
    }

    public int getId() { return id; }

    public void attach(Transform parent) { transform.SetParent(parent, true); }

}
