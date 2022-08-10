using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;

    


    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            isDead = true;

        }
        if(health >= maxHealth)
        {

            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void SetHealthTo(float healthSetTo)
    {
        health = healthSetTo;
        CheckHealth();

    }

    public virtual void TakeDamage(float damage)
    {
        float healtAfterDamage = health - damage;
        SetHealthTo(healtAfterDamage);
        
    }

    public void Heal(float heal)
    {
        float healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
   
}
