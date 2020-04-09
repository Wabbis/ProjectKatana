using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WatchState : BaseState
{
    private RangedEnemy _rangedEnemy;

    public WatchState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
    }

    public override Type Tick()
    {
        if (_rangedEnemy._target)
        {
            if (!_rangedEnemy.CheckObstacles(_rangedEnemy._target))
            {
                return typeof(ChaseState);
            }
        }

        return null;
    }
}
