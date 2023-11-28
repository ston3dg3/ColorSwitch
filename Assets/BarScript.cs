using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxBar(int value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetBar(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
