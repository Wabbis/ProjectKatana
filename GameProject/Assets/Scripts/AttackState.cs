using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackState : BaseState
{ 
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

        return null;
    }
}
