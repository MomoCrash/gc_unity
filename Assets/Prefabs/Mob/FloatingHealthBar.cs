using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    // Start is called before the first frame update

    public void UpdateHealthBar(float currentValue, float MaxValue)
    {
        slider.value = currentValue/MaxValue;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}