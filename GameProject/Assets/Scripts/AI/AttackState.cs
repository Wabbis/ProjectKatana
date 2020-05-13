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
        if (!_rangedEnemy._target || _rangedEnemy.CheckObstacles(_rangedEnemy._target))
        {
            
            
                if (_rangedEnemy.patrolling)
                {
                    _rangedEnemy.animator.SetTrigger("Walk");
                    return typeof(PatrolState);
                }
                else
                {
                    _rangedEnemy.animator.SetTrigger("Idle");
                    return typeof(WatchState);
                }
            
       

        }

        float distance = Vector2.Distance(transform.position, _rangedEnemy._target.transform.position);

        if (_rangedEnemy.melee && distance > _rangedEnemy.meleeAttackRange)
        {
            _rangedEnemy.animator.SetTrigger("Walk");
            return typeof(ChaseState);
        }
        //katsoo pelaajaa kohti
        Vector3 vectorToTarget = _rangedEnemy._target.transform.position - _rangedEnemy.eyes.transform.position;
        Vector3 gunVector = _rangedEnemy._target.transform.position - _rangedEnemy.gunRotator.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        float gunAngle = Mathf.Atan2(gunVector.y, gunVector.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion q2 = Quaternion.AngleAxis(gunAngle, Vector3.forward);
        _rangedEnemy.eyes.transform.rotation = Quaternion.Slerp(_rangedEnemy.eyes.transform.rotation, q, Time.deltaTime * 100);
        _rangedEnemy.gunRotator.transform.rotation = Quaternion.Slerp(_rangedEnemy.gunRotator.transform.rotation, q2, Time.deltaTime * 100);

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
            if (_rangedEnemy.patrolling)
            {
                _rangedEnemy.animator.SetTrigger("Walk");
                return typeof(PatrolState);
            }
            else
            {
                _rangedEnemy.animator.SetTrigger("Idle");
                return typeof(WatchState);
            }

        }

        // _attackCooldown -= Time.deltaTime;
        if (_rangedEnemy.melee)
        {
            _rangedEnemy.firstMelee -= Time.deltaTime;
            if (_rangedEnemy.firstMelee < 0)
            {
                _rangedEnemy.Attack(_rangedEnemy._target);
                _rangedEnemy.firstMelee = _rangedEnemy.meleeCD;
            }
        }
        else
        {
            _rangedEnemy.firstShot -= Time.deltaTime;
            if (_rangedEnemy.firstShot < 0)
            { 
                _rangedEnemy.Attack(_rangedEnemy._target);
            _rangedEnemy.firstShot = _rangedEnemy.reloadTime;

                
            }
        }

        /*if (_attackCooldown <= 0f)
        {
           // Debug.Log("ATTACK!");
            _rangedEnemy.Attack(_rangedEnemy._target);
            _attackCooldown = _rangedEnemy.reloadTime;
        }*/


        return null;
    }
}
