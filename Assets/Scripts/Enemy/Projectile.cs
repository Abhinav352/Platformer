using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 10;

    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        Move();
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply damage to the player or perform other actions
            collision.GetComponent<CharacterHealth>().TakeDamage(damage);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
