using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject _target { get; private set; }
    public float attackRange = 3f;
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
            {typeof(AttackState), new AttackState(rangedEnemy: this) },
            {typeof(AlertState), new AlertState(rangedEnemy: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    public void Attack(GameObject target)
    {
        // Do attack stuff
        Destroy(target);
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }

    public bool CheckObstacles(GameObject target)
    {
        // Make Layer Mask for Environment and check if you get any returns from that layer. No need to check every object 
        int layermask2 = (LayerMask.GetMask("Environment"));
        Vector2 dir = target.transform.position - transform.position;
        float rayDistance = Vector2.Distance(target.transform.position, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayDistance, layermask2);
        Debug.DrawRay(transform.position, _target.transform.position - transform.position, Color.red); // draw line in the Scene window to show where the raycast is looking
        if (hit == false)
            return false;
        else
        {
            Debug.Log("Something in the way...");
            return true;
        }

        /* float distanceToPlayer = Vector2.Distance(eyes.transform.position, _target.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(eyes.transform.position, _target.transform.position - eyes.transform.position, distanceToPlayer);
        Debug.DrawRay(eyes.transform.position, _target.transform.position - _eyes.transform.position, Color.red); // draw line in the Scene window to show where the raycast is looking

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
        */


    }


}
