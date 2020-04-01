using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackState : BaseState
{
    private float _attackCooldown;
    private RangedEnemy _rangedEnemy;

    public AttackState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {


        if (_rangedEnemy == null)
        {
            return typeof(PatrolState);
        }
        _attackCooldown -= Time.deltaTime;

        if (_attackCooldown <= 0f)
        {
            Debug.Log("ATTACK!");
            _rangedEnemy.Attack();
        }

      
        return null;
    }
}
