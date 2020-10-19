using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Text indicator;

    public void updateIndicator(float single)
    {
        indicator.text = single.ToString();
    }
}
