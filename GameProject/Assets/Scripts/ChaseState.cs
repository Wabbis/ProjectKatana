using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private RangedEnemy _rangedEnemy;
    private Vector2 direction;
    private float chaseSpeed = 10;
    public ChaseState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {

        /* if (!_rangedEnemy._target || _rangedEnemy.CheckObstacles())

        {
            return typeof(PatrolState);
        }
        
        if (_rangedEnemy._target.transform.position.x < _rangedEnemy.transform.position.x)
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        */

        // Vector3 objectPos = _rangedEnemy.eyes.transform.position;
        if (_rangedEnemy._target != null)
        {
            Vector3 vectorToTarget = _rangedEnemy._target.transform.position - _rangedEnemy.eyes.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            _rangedEnemy.eyes.transform.rotation = Quaternion.Slerp(_rangedEnemy.eyes.transform.rotation, q, Time.deltaTime * 100);
        }

        if (_rangedEnemy == null)
        {
            Debug.Log("We didnt have a target");
            return typeof(PatrolState);
        }

           if ((_rangedEnemy.transform.position.x - _rangedEnemy._target.transform.position.x) < 0)
               direction = Vector2.right;
           //tämä ei toimi jos pelaaja on vihollisen vasemmalla puolella( vihollinen lähtee pakittamaan).
           //else
             // direction = Vector2.right * -1;
        
        // _rangedEnemy.transform.position = Vector3.MoveTowards(_rangedEnemy.transform.position, new Vector3(_rangedEnemy._target.transform.position.x, _rangedEnemy.transform.position.y, _rangedEnemy.transform.position.z), chaseSpeed * Time.deltaTime);

        _rangedEnemy.transform.Translate(direction * chaseSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, _rangedEnemy._target.transform.position);
        if (distance < _rangedEnemy.attackRange)
        {
            return typeof(AttackState);
        }
       
        if (!_rangedEnemy._target ||_rangedEnemy.CheckObstacles(_rangedEnemy._target))
        { 
            // Make enemy to wait for few seconds before going back to patrol - Wait Function doesnt work yet.
            _rangedEnemy.StartCoroutine("Wait");
            Debug.Log("Target lost. Going to Patrol");
            return typeof(PatrolState);
        }
        return null;
    }
}
