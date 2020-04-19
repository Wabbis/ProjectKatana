using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    private int nextWaypoint = 0;
    public float speed = 4;
    public float chargeSpeed = 20;
    public Transform target;
    Vector3 targetPos;
    public bool charging;
    RaycastHit2D hit;
    public float waitTime;
    float timeLeft;
    int layermask2;

    private void Start()
    {
        layermask2 = (LayerMask.GetMask("Player"));
        timeLeft = waitTime;
    }
    void Update()
    {



        if (!charging)
        {
            Patrol();
            hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), Mathf.Infinity, layermask2);
            if (hit)
            {
                charging = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                targetPos = new Vector2(hit.transform.position.x, transform.position.y);

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.white);

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, chargeSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    charging = false;
                    timeLeft = waitTime;
                }
            }
        }


    }

    void Patrol()
    {

        float step = speed * Time.deltaTime;

        target = waypoints[nextWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);



        if (transform.position.x < target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {

            nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
            target = waypoints[nextWaypoint];


        }

    }
}
