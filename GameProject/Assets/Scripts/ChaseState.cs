using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private RangedEnemy _rangedEnemy;
    private Vector2 direction;
    private float chaseSpeed = 5;
    public ChaseState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {

        Debug.DrawRay(_rangedEnemy.transform.position, direction * _rangedEnemy._rayDistance, Color.green);
        Debug.DrawRay(_rangedEnemy.transform.position, direction * _rangedEnemy.evadeRange, Color.blue);
        Debug.DrawRay(_rangedEnemy.transform.position, direction * _rangedEnemy.attackRange, Color.red);



        if (_rangedEnemy == null)
        {
            return typeof(PatrolState);
        }

        if ((_rangedEnemy.transform.position.x - _rangedEnemy._target.transform.position.x) < 0)
            direction = Vector2.right;
        else
            direction = Vector2.right * -1;

        _rangedEnemy.transform.Translate(direction * chaseSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, _rangedEnemy._target.transform.position);
        if (distance < _rangedEnemy.attackRange)
        {
            return typeof(AttackState);
        }

        if (distance > _rangedEnemy.evadeRange)
        {
            return typeof(PatrolState);
        }
        return null;
    }
}
