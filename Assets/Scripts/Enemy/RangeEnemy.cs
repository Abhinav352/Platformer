using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public Transform character;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float shootingRange = 5f;
    public float moveSpeed = 3f;
    public float retreatDistance = 2f;
    public float shootingDelay = 2f;

    private bool isCharacterInRange = false;

    private void Update()
    {
        if (isCharacterInRange)
        {
            // Move away from the character if within shooting range
            if (Vector2.Distance(transform.position, character.position) <= shootingRange)
            {
                Vector2 moveDirection = transform.position + character.position;
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

                // Shoot projectile with delay
                if (Time.time >= shootingDelay)
                {
                    ShootProjectile();
                    shootingDelay = Time.time + shootingDelay;
                }
            }
            else if (Vector2.Distance(transform.position, character.position) <= retreatDistance)
            {
                // Move away from the character if within retreat distance
                Vector2 moveDirection = transform.position - character.position;
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Move towards the character
                Vector2 moveDirection = character.position - transform.position;
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCharacterInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCharacterInRange = false;
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        // Set the projectile's direction towards the character
        Vector2 direction = (character.position - projectileSpawnPoint.position).normalized;
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

}