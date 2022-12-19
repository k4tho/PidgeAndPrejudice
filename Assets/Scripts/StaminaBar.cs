using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public void SetStamina(float stamina)
    {
        slider.value = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
