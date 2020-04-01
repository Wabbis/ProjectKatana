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


    public GameObject eyes;

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
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        transform.Rotate(0, 180, 0);
    }

    public void Attack()
    {
        // Do attack stuff

    }

    public bool CheckObstacles()
    {
        float distanceToPlayer = Vector2.Distance(eyes.transform.position, _target.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(eyes.transform.position, _target.transform.position - eyes.transform.position, distanceToPlayer);
        Debug.DrawRay(eyes.transform.position, _target.transform.position - eyes.transform.position, Color.red); // draw line in the Scene window to show where the raycast is looking


        foreach (RaycastHit2D hit in hits)
        {
            // ignore the enemy's own colliders (and other enemies)
            if (hit.transform.tag == "Enemy")
                continue;

            // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
            if (hit.transform.tag != "Player")
            {
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)

        return false;

    }
}
