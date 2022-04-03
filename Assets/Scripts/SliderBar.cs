using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderBar : MonoBehaviour
{
    [SerializeField] private Slider m_slider;

    public void SetValue(float i)
    {
        m_slider.value = i;
    }
}
