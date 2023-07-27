using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : MonoBehaviour
{
    public float speed = 5f;
    public float aggroSpeed = 10f;
    public float aggroRadius = 5f;
    public float aggroTime = 5f;

    private bool isAggroed = false;
    private float aggroTimer = 0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isAggroed)
        {
            aggroTimer -= Time.deltaTime;

            if (aggroTimer <= 0f)
            {
                isAggroed = false;
                aggroTimer = 0f;
                transform.position = startPosition;
            }
            else
            {
                Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
                transform.position += direction * aggroSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) <= aggroRadius)
            {
                isAggroed = true;
                aggroTimer = aggroTime;
            }
        }
    }
}
