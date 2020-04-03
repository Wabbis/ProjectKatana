using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AlertState : BaseState
{
    private RangedEnemy _rangedEnemy;


    public AlertState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {
        _rangedEnemy.GetComponent<SpriteRenderer>().color = Color.red;
        if(_rangedEnemy._target == null)
        {
            Debug.Log("Target is dead.");
            return typeof(PatrolState);
        }

        float distance = Vector2.Distance(transform.position, _rangedEnemy._target.transform.position);
        if (distance < _rangedEnemy.attackRange)
        {
            return typeof(AttackState);
        }

        return null;
    }
}
