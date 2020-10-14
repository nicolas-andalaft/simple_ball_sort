using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraCentralizer : MonoBehaviour
{
    [SerializeField] private float yOffset;

    public void centralize(LevelFactory levelFactory)
    {
        float xMax = (levelFactory.getBottleRowQty() - 1) * levelFactory.getBottleMargin();
        float yMax = (levelFactory.getBottlesQty() - 1) / levelFactory.getBottleRowQty() * -0.5f;

        Camera.main.transform.position = new Vector3(xMax / 2, (yMax / 2) + yOffset, -10);
    }
}
