using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraCentralizer : MonoBehaviour
{
    [SerializeField] private float yOffset;

    public void centralize(LevelFactory levelFactory)
    {
        float xMax = (levelFactory.bottleRowQty - 1) * levelFactory.bottleMargin;
        float yMax = (levelFactory.ballTypes - 1) / levelFactory.bottleRowQty * -0.5f;

        Camera.main.transform.position = new Vector3(xMax / 2, (yMax / 2) + yOffset, -10);
    }
}
