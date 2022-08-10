using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public GameObject gameOverMenu;
    private bool isDead;

    public Slider healthBar;


    

    private void Awake()
    {
       
        healthBar.maxValue = maxHealth; 
        currentHealth = maxHealth;
       gameOverMenu.SetActive(false);

    }

    public void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {

       
        currentHealth -= damage;
       
        if (currentHealth <= 0)
        {
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        isDead = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverMenu.SetActive(true);
        
    }
}
