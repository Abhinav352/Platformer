using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10;

  
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                CharacterHealth characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
                if (characterHealth != null)
                {
                    characterHealth.TakeDamage(damageAmount);
                }
            }
        }
    }