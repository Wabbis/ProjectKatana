using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private RangedEnemy _rangedEnemy;
    
    public ChaseState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {
        
        if(_rangedEnemy == null)
        {
            return typeof(PatrolState);
        }

        if(_rangedEnemy._target.transform.position.x < _rangedEnemy.transform.position.x)
        {
            ;
        }

        _rangedEnemy.transform.Translate(_rangedEnemy._direction);

        return null;
    }
}
