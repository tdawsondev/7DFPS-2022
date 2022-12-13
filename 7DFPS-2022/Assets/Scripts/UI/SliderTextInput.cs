using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SliderTextInput : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_InputField textBox;
    public float minValue, maxValue;
    public float value;

    public UnityEvent OnValueChange;
    // Start is called before the first frame update
    void Start()
    {
        //slider.minValue = minValue;
       // slider.maxValue = maxValue;
    }

    public void OnSliderChange()
    {
        textBox.SetTextWithoutNotify("" + slider.value);
        value = slider.value;
        OnValueChange.Invoke();
    }
    public void OnTextChange()
    {
        float value = float.Parse(textBox.text);
        if (value > maxValue)
        {
            textBox.SetTextWithoutNotify(maxValue + "");
        }
        else if (value < minValue)
        {
            textBox.SetTextWithoutNotify(minValue + "");
        }
        else
        {
            slider.SetValueWithoutNotify(value);
            textBox.SetTextWithoutNotify("" + value);
        }
        this.value = value;
        OnValueChange.Invoke();
    }
    
    public void SetValueWithoutNotify(float value)
    {
        slider.SetValueWithoutNotify(value);
        textBox.SetTextWithoutNotify(value+"");
    }
}
