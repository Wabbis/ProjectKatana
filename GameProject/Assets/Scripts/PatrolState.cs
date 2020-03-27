using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PatrolState : BaseState
{

    private float stopDistance = 1f;
    private float _rayDistance = 2f;
    private RangedEnemy _rangedEnemy;
    private GameObject[] _waypoints;
    private GameObject nextWaypoint;
    private float distance;
    
    public PatrolState(RangedEnemy rangedEnemy, GameObject[] waypoints) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
        _waypoints = waypoints;
        nextWaypoint = waypoints[0];
    } 

    public override Type Tick()
    {

        Debug.DrawLine(_rangedEnemy.transform.position, _rangedEnemy.facingDirection.transform.position);
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null)
        {
            _rangedEnemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        distance = Vector2.Distance(_rangedEnemy.transform.position, nextWaypoint.transform.position);
        if (distance < stopDistance)
        {
            if (nextWaypoint == _waypoints[0])
            {
                nextWaypoint = _waypoints[1];
                _rangedEnemy._direction *= -1;
                _rangedEnemy.Flip();
            }
            else
            {
                nextWaypoint = _waypoints[0];
                _rangedEnemy._direction *= -1;
                _rangedEnemy.Flip();
            }
        }

        _rangedEnemy.transform.Translate(_rangedEnemy._direction);

        return null;
    }

    public GameObject CheckForAggro()
    {
        Ray ray = new Ray(_rangedEnemy.transform.position, _rangedEnemy.facingDirection.transform.position);
        
        if(Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _rangedEnemy.layerMask))
            {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Player")
                {
                    return hit.collider.gameObject;
                }
            }
        }
        return null;
    }
}
