using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie2Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Zombie2 zombie2;

    private void Awake()
    {
        zombie2 = GetComponentInParent<Zombie2>();
    }

    private void Start()
    {
        healthSlider.maxValue = zombie2.presentHealth;
        healthSlider.value = zombie2.presentHealth;
        healthSlider.minValue = 0f;
    }

    public void ReducedHealthSlider(float giveDamge)
    {
        healthSlider.value -= giveDamge;

        if (healthSlider.value <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
