using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void SetHealthSlider(float percent)
    {
        healthSlider.value = percent;
    }
}
