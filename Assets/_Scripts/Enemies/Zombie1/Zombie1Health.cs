using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie1Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Zombie1 zombie1;

    private void Awake()
    {
        zombie1 = GetComponentInParent<Zombie1>();
    }

    private void Start()
    {
        healthSlider.maxValue = zombie1.presentHealth;
        healthSlider.value = zombie1.presentHealth;
        healthSlider.minValue = 0f;
    }

    public void ReducedHealthSlider(float giveDamge)
    {
        healthSlider.value -= giveDamge;

        if(healthSlider.value <= 0f)
        {
            gameObject.SetActive(false);
        }    
    }    
}
