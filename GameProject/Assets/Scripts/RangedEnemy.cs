using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject _target { get; private set; }
    public float attackRange = 0.2f;
    public float evadeRange = 10f;
    public float _rayDistance = 7f;

    private void Awake()
    {
        InitStateMachine();
        Debug.Log("Enemy woken");
    }

    private void InitStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(PatrolState), new PatrolState(rangedEnemy: this, waypoints) },
            {typeof(ChaseState), new ChaseState(rangedEnemy: this) },
            {typeof(AttackState), new AttackState(rangedEnemy: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
    public void Attack()
    {
        // Do attack stuff

    }
}
