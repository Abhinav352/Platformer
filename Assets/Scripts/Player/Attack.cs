using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackRadius = 2f;
    public int attackDamage = 10;
    public KeyCode attackKey = KeyCode.Space;
    public GameObject damagePopupPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            foreach (Collider2D collider in colliders)
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);

                    // Instantiate a damage pop-up object
                    GameObject damagePopup = Instantiate(damagePopupPrefab, collider.transform.position, Quaternion.identity);
                   
                    DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
                    if (damagePopupScript != null)
                    {
                        damagePopupScript.ShowDamage(attackDamage);
                        ScoreManager.Instance.AddScore(attackDamage);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}