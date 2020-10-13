using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraCentralizer : MonoBehaviour
{
    [SerializeField] private float yOffset;

    public void centralize(float xMax, float yMax)
    {
        Camera.main.transform.position = new Vector3(xMax / 2, (yMax / 2) + yOffset, -10);
    }
}
