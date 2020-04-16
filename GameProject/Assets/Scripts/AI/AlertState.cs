using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AlertState : BaseState
{
    private RangedEnemy _rangedEnemy;
    private Vector2 direction;
    private GameObject focusTarget;

    public float chaseSpeed = 10;


    public AlertState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }
    public override Type Tick()
    {
        _rangedEnemy.GetComponent<SpriteRenderer>().color = Color.red;
        if(focusTarget == null)
        {
            Debug.Log("Target lost");
            if (_rangedEnemy.patrolling)
            {
                return typeof(PatrolState);
            }
            else
            {
                return typeof(WatchState);
            }
        }

        if ((_rangedEnemy.transform.position.x - _rangedEnemy._target.transform.position.x) < 0)
            direction = Vector2.right;
        else
            direction = Vector2.right * -1;

        _rangedEnemy.transform.Translate(direction * chaseSpeed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, _rangedEnemy._target.transform.position);
        if (distance < _rangedEnemy.meleeAttackRange)
        {
            return typeof(AttackState);
        }

        return null;
    }
}
