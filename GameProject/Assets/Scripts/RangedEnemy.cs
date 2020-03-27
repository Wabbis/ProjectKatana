using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject facingDirection;
    public Vector2 _direction;
    public GameObject[] waypoints;
    public GameObject _target { get; private set; }

    void Start()
    {
        _direction = Vector2.right * Time.deltaTime * 1.0f;
        int i = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Waypoint")) 
        {
            waypoints[i] = go;
            i++;
        }
    }

    private void Awake()
    {
        InitStateMachine();
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
    
}
