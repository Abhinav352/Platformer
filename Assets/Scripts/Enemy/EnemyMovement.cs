using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyMovement : MonoBehaviour
{
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float patrolSpeed = 2;
    public float chaseSpeed = 4;
    public float spottingDistance = 5f;
    public float returnDistance = 10f;
    private float duration = 1f;

    private Transform player;
    
    private bool isChasing = false;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = GetComponent<Rigidbody2D>();
    }

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        //Make box collider trigger


        //Create Root object
        GameObject root = new GameObject(name + "_Root");
        //Reset Position of Root to enemy object
        root.transform.position = transform.position;
        //Set enemy object as child of root
        transform.SetParent(root.transform);
        //Create Waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //Reset waypoints position to root        
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //Create two points (gameobject) and reset their position to waypoints objects
        //Make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
       
        Transform goalPoint = points[nextID];

        //Flip the enemy transform to look into the point's direction
      
        if (goalPoint.transform.position.x > transform.position.x)
        {
            
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }
        else
        {
           
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        }
        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, patrolSpeed * Time.deltaTime);
        //Check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
           
            //Check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //Check if we are at the start of the line (make the change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply the change on the nextID
            nextID += idChangeValue;
        }
    }

    void ChasePlayer()
    {
        if (player == null)
        {
            isChasing = false;
            return;
        }

        if (Vector2.Distance(transform.position, player.position) > returnDistance)
        {
            // Player is too far, go back to patrolling state
            isChasing = false;
            return;
        }

        if (Vector2.Distance(transform.position, player.position) <= spottingDistance)
        {
           
            // Player is within the spotting distance, move towards the player
            if (player.position.x > transform.position.x)
            {
                
                transform.localScale = new Vector3(0.2f, 0.2f, 1);
            }
            else
            {
                
                transform.localScale = new Vector3(-0.2f, 0.2f, 1);
            }

            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }
  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spottingDistance);
    }
}