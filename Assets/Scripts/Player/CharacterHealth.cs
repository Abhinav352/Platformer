using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
      
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
            
        }
    }

    IEnumerator Die()
    {
        // Perform any actions when the character dies
        // For example, play death animation, show game over screen, etc.
        currentHealth = 0;
        
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(gameObject);
    }
}