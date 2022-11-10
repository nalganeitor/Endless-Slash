using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;

    void Start()
    {
        SetMaxHealth();
    }

    void Update()
    {
        SetHealth();
    }

    public void SetMaxHealth()
    {
        slider.maxValue = playerHealth._playerCurrentHealth;
        slider.value = playerHealth._playerCurrentHealth;
        Debug.Log("fight");

    }

    public void SetHealth()
    {
        slider.value = playerHealth._playerCurrentHealth;
        Debug.Log("auch");
    }
}
