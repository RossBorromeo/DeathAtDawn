using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBehaviour : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public Text healthPercentage;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;
        
    }
    
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        
        
    }
    public void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.value = currentHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentHealth > 0)
            {
                TakeDamage(10);
            }
        }
    }
    public void OnHealthSliderChanged(float value)
    {
         healthPercentage.text = healthBar.value.ToString("0.0");
    }
}

