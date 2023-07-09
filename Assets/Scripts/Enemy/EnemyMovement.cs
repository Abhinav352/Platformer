using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float followSpeed = 2f;
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public float pointSwitchDelay = 2f;

    private bool isFollowing = false;
    private bool isReturning = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float switchTime;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = pointB.position;
        switchTime = Time.time + pointSwitchDelay;
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= detectionRadius)
            {
                isFollowing = true;
                isReturning = false;
            }
            else
            {
                isFollowing = false;
                isReturning = true;

                if (transform.position != initialPosition)
                {
                    targetPosition = initialPosition;
                    switchTime = Time.time + pointSwitchDelay;
                }
            }

            if (isFollowing)
            {
                targetPosition = player.position;
                targetPosition.y = transform.position.y; // Keep the same y-position
                switchTime = Time.time + pointSwitchDelay;
            }
        }

        if (!isFollowing && Time.time >= switchTime)
        {
            if (isReturning)
            {
                if (transform.position == initialPosition)
                {
                    isReturning = false;
                    targetPosition = pointB.position;
                }
                else
                {
                    targetPosition = initialPosition;
                }
            }
            else
            {
                if (transform.position == pointA.position)
                {
                    targetPosition = pointB.position;
                }
                else if (transform.position == pointB.position)
                {
                    targetPosition = pointA.position;
                }
            }

            switchTime = Time.time + pointSwitchDelay;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection radius gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw line between point A and point B
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}

