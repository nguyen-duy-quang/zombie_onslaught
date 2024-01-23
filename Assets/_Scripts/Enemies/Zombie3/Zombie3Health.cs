using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie3Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Zombie3 zombie3;

    private void Awake()
    {
        zombie3 = GetComponentInParent<Zombie3>();
    }

    private void Start()
    {
        healthSlider.maxValue = zombie3.presentHealth;
        healthSlider.value = zombie3.presentHealth;
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
