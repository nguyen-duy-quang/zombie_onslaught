using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster1Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Monster1 monster1;

    private void Awake()
    {
        monster1 = GetComponentInParent<Monster1>();
    }

    private void Start()
    {
        healthSlider.maxValue = monster1.presentHealth;
        healthSlider.value = monster1.presentHealth;
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
