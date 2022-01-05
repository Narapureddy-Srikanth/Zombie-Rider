using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Slider healthSlider;
    public Image fill;
    public Gradient healthGradient;
    // Start is called before the first frame update
    void Start()
    {
        fill.color = healthGradient.Evaluate(1f);
    }

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealthSlider(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }

    public void SetHealthSlider(float health)
    {
        healthSlider.value = health;
        fill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
}
