﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PatrolState : BaseState
{

    private float stopDistance = 1f;

    private RangedEnemy _rangedEnemy;
    private GameObject[] _waypoints;
    private GameObject nextWaypoint;
    private float distance;
    private Vector2 direction;
    private Vector2 nextLocation;
    private float speed = 5f;

    public PatrolState(RangedEnemy rangedEnemy, GameObject[] waypoints) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
        _waypoints = waypoints;
        nextWaypoint = waypoints[0];
    }

    public override Type Tick()
    {
        /*   if ((_rangedEnemy.transform.position.x - nextWaypoint.transform.position.x) < 0) 
               direction = Vector2.right;
           else
               direction = Vector2.right * -1;
               */



        Debug.DrawRay(_rangedEnemy.transform.position, direction * _rangedEnemy._rayDistance, Color.green);
        Debug.DrawRay(new Vector2(_rangedEnemy.transform.position.x, _rangedEnemy.transform.position.y + 1), direction * _rangedEnemy.evadeRange, Color.blue);
        Debug.DrawRay(new Vector2(_rangedEnemy.transform.position.x, _rangedEnemy.transform.position.y + 2), direction * _rangedEnemy.attackRange, Color.red);



        /*  // var chaseTarget = CheckForAggro();
          var chaseTarget = CheckForAggro();
          if (chaseTarget != null)
          {
              _rangedEnemy.SetTarget(chaseTarget);
              return typeof(ChaseState);
          }*/


        if (_rangedEnemy._target)
        {

            if (!_rangedEnemy.CheckObstacles())
            {
                return typeof(ChaseState);
            }
        }
        _rangedEnemy.transform.position = Vector3.MoveTowards(_rangedEnemy.transform.position, nextWaypoint.transform.position, speed * Time.deltaTime);

        distance = Vector2.Distance(_rangedEnemy.transform.position, nextWaypoint.transform.position);
        if (distance < stopDistance)
        {
            if (nextWaypoint == _waypoints[0])
            {
                // _rangedEnemy.Flip();
                nextWaypoint = _waypoints[1];

            }
            else
            {
                //  _rangedEnemy.Flip();
                nextWaypoint = _waypoints[0];


            }
        }

        if (_rangedEnemy.transform.position.x < nextWaypoint.transform.position.x)
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        _rangedEnemy.eyes.transform.rotation = _rangedEnemy.transform.rotation;


        // _rangedEnemy.transform.Translate(direction * speed * Time.deltaTime);

        return null;
    }

    public GameObject CheckForAggro()
    {
        int layermask = ~(LayerMask.GetMask("Enemy"));

        RaycastHit2D hit = Physics2D.Raycast(_rangedEnemy.transform.position, direction, _rangedEnemy._rayDistance, layermask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            return hit.collider.gameObject;
        }
        return null;
    }


}
