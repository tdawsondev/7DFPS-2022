using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider ManaSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateManaSlider(int currentMana, int maxMana)
    {
        ManaSlider.maxValue = maxMana;
        ManaSlider.value = currentMana;
    }
}
