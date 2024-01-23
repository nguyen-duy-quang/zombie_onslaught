using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster2Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Monster2 monster2;

    private void Awake()
    {
        monster2 = GetComponentInParent<Monster2>();
    }

    private void Start()
    {
        healthSlider.maxValue = monster2.presentHealth;
        healthSlider.value = monster2.presentHealth;
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
