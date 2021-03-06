﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PatrolState : BaseState
{
    private float stopDistance = 2f;
    private RangedEnemy _rangedEnemy;    
    private GameObject[] _waypoints;
    private GameObject nextWaypoint;
    private float distance;
    private Vector2 nextLocation;
    private float speed = 5f;

    int delay = 5;
    int delayLeft;

    public PatrolState(RangedEnemy rangedEnemy, GameObject[] waypoints) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
        _waypoints = waypoints;
        nextWaypoint = waypoints[0];
        
    }


    public override Type Tick()
    {
        _rangedEnemy.animator.SetTrigger("Walk");

        if (_rangedEnemy._target)
        {
            if (!_rangedEnemy.CheckObstacles(_rangedEnemy._target))
            {
                if (_rangedEnemy.melee)
                    return typeof(ChaseState);
                else
                    return typeof(AttackState);
            }
        }
               
        _rangedEnemy.transform.position = Vector3.MoveTowards(_rangedEnemy.transform.position, nextWaypoint.transform.position, speed * Time.deltaTime);

         distance = Vector2.Distance(_rangedEnemy.transform.position, nextWaypoint.transform.position);
        if (distance < stopDistance)
        {
            if (nextWaypoint == _waypoints[0])
                nextWaypoint = _waypoints[1];
            else
                nextWaypoint = _waypoints[0];
        }

        if(_rangedEnemy.transform.position.x < nextWaypoint.transform.position.x)
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        
        _rangedEnemy.eyes.transform.rotation = _rangedEnemy.transform.rotation;

        //palauttaa silmän reunalta paluun jälkeen
      /*  if (!_rangedEnemy.eyes.activeInHierarchy)
        {
            delayLeft--;
            if (delayLeft < 0)
            {
                _rangedEnemy.eyes.SetActive(true);
                delayLeft = delay;
            }

        }*/

        return null;
    }
}
