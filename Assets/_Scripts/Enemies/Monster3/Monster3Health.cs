using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster3Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Monster3 monster3;

    private void Awake()
    {
        monster3 = GetComponentInParent<Monster3>();
    }

    private void Start()
    {
        healthSlider.maxValue = monster3.presentHealth;
        healthSlider.value = monster3.presentHealth;
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
