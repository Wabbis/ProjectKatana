﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject explosion;
    public bool exploding;
    public bool killable = true;
    bool dead;
    public Image indicator;
    public float explosionSize=5;
    Animator animator;
    public GameObject light1;
    public GameObject light2;


    public float rayLength;

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        layermask2 = (LayerMask.GetMask("Player"));
        timeLeft = waitTime;
        indicator.gameObject.SetActive(false);
    }
    void Update()
    {
        indicator.fillAmount = timeLeft / waitTime;

        if (!dead)
       {
            if (!charging)
            {
                Patrol();
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), rayLength, layermask2);
                if (hit)
                {
                    animator.SetTrigger("Charge");
                    charging = true;
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                    targetPos = new Vector2(hit.transform.position.x, transform.position.y);

                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * rayLength, Color.white);

                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, chargeSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPos) < 0.001f)
                {
                    if (exploding)
                    {
                        indicator.gameObject.SetActive(true);
                        animator.SetTrigger("Die");
                    }
                    animator.SetTrigger("Idle");
                    timeLeft -= Time.deltaTime;

                    if (timeLeft < 0)
                    {

                        if (exploding)
                        {
                            Destroy(indicator.gameObject);
                            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
                            expl.transform.localScale = expl.transform.localScale * explosionSize;
                            Destroy(expl.gameObject, 1);
                            transform.DetachChildren();
                            Destroy(gameObject);

                        }
                        animator.SetTrigger("Patrol");
                        charging = false;
                        timeLeft = waitTime;
                    }
                }
            }

        }

       


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (charging)
        {
            if (collision.transform.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerControls>().Die();
                /* if (!collision.gameObject.GetComponent<PlayerControls>().block)
                 {


                     Destroy(collision.gameObject);
                   //  Destroy(gameObject);
                 }*/
            }
        }
  
    }

    public void Die()
    {
        dead = true;
        Debug.Log("Die");
        animator.SetTrigger("Explode");
        Destroy(gameObject,0.5f);
        light1.SetActive(false);
        light2.SetActive(false);
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
