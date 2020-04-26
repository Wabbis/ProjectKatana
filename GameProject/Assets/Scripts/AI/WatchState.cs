using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WatchState : BaseState
{
    private RangedEnemy _rangedEnemy;
    float timeLeft;
    int delay=5;
    int delayLeft;

    public WatchState(RangedEnemy rangedEnemy) : base(rangedEnemy.gameObject)
    {
        _rangedEnemy = rangedEnemy;
        timeLeft = _rangedEnemy.turnInterval;
        delayLeft = delay;
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



        if (transform.position.x != _rangedEnemy.startPos.x)      
        {
            if (transform.position.x > _rangedEnemy.startPos.x)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            transform.position = Vector2.MoveTowards(transform.position, _rangedEnemy.startPos, 5f * Time.deltaTime);
        }
        else if (_rangedEnemy.turning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                _rangedEnemy.Flip();
                timeLeft = _rangedEnemy.turnInterval;
            }

        }

        if (_rangedEnemy.eyes.transform.rotation.z != 0)
        {
            _rangedEnemy.eyes.transform.rotation = Quaternion.Euler(_rangedEnemy.transform.rotation.x, _rangedEnemy.transform.rotation.y, 0);
        }


     


        //palauttaa silmän reunalta paluun jälkeen
        if (!_rangedEnemy.eyes.activeInHierarchy)
        {
            delayLeft--;
            if (delayLeft < 0)
            {
                _rangedEnemy.eyes.SetActive(true);
                delayLeft = delay;
            }
                
        }

        return null;
    }
}
