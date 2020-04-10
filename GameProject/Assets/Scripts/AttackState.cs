using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackState : BaseState
{

    private float _attackCooldown=0.1f;
    private RangedEnemy _rangedEnemy;

    public AttackState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {
        if (!_rangedEnemy._target)
        {
            return typeof(PatrolState);
        }

        //katsoo pelaajaa kohti
        Vector3 vectorToTarget = _rangedEnemy._target.transform.position - _rangedEnemy.eyes.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        _rangedEnemy.eyes.transform.rotation = Quaternion.Slerp(_rangedEnemy.eyes.transform.rotation, q, Time.deltaTime * 100);
        _rangedEnemy.gunRotator.transform.rotation = Quaternion.Slerp(_rangedEnemy.gunRotator.transform.rotation, q, Time.deltaTime * 100);

        //kääntää vihollisen kohti pelaajaa
        if (_rangedEnemy._target.transform.position.x < _rangedEnemy.transform.position.x)
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _rangedEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (_rangedEnemy == null)
        {
            return typeof(PatrolState);
        }

        _attackCooldown -= Time.deltaTime;

        if (_attackCooldown <= 0f)
        {
            Debug.Log("ATTACK!");
            _rangedEnemy.Attack(_rangedEnemy._target);
            _attackCooldown = _rangedEnemy.reloadTime;
        }


        return null;
    }
}
