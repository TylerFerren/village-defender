using UnityEngine;
using UnityEngine.UI;

public class UiSliders : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float value) {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetValue(float value) {
        slider.value = value;
    }
}
