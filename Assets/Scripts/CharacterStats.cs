using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
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

    public void SetHealthTo(int healthSetTo)
    {
        health = healthSetTo;
        CheckHealth();

    }

    public virtual void TakeDamage(int damage)
    {
        int healtAfterDamage = health - damage;
        SetHealthTo(healtAfterDamage);
        
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
   
}
