using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public bool patrolling;
    public bool melee;
    public bool turning; //jos tosi, paikallaan oleva vihollinen kääntyy tasaisinväliajoin
    public Vector2 startPos;

    public GameObject[] waypoints;
    public GameObject _target { get; private set; }
    public float meleeAttackRange = 1f;
    public float evadeRange = 10f;
    public float _rayDistance = 7f;
    public float chaseSpeed;
    public LayerMask enemyLayers;
    public float turnInterval=5f;

    public GameObject edgeCheck;
    public GameObject eyes;
    public Transform meleeAttackPoint;
    public GameObject gunRotator;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject light;

    public float bulletSpeed = 20;
    public float firstMelee=0.5f;
    public float meleeCD=1;
   public float firstShot= 0.1f;
    public float reloadTime= 1;

    public Animator animator;
    public bool dead;

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        InitStateMachine();
        Debug.Log("Enemy woken");
    }

    private void InitStateMachine()
    {
        if (patrolling)
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
        else
        {
            var states = new Dictionary<Type, BaseState>()
            {
            {typeof(WatchState), new WatchState(rangedEnemy: this) },    
            {typeof(ChaseState), new ChaseState(rangedEnemy: this) },
            {typeof(AttackState), new AttackState(rangedEnemy: this) },
            {typeof(AlertState), new AlertState(rangedEnemy: this) }
            };
            GetComponent<StateMachine>().SetStates(states);
        }
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
        animator.SetTrigger("Attack");
        if (!melee)
        {
           // Shoot();
        }
        else
        {
           // Hit();
        }
       // animator.SetTrigger("Idle");

     
    }
    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        newBullet.GetComponent<EnemyBullet>().dir = (bulletSpawn.transform.position - gunRotator.transform.position)*bulletSpeed * 0.01f;
        //   newBullet.GetComponent<Rigidbody2D>().AddForce((bulletSpawn.transform.position - gunRotator.transform.position) * bulletSpeed*0.0001f, ForceMode2D.Impulse);
        SoundManager.PlaySound("PISTOLENEMY");
        Destroy(newBullet, 10);

    }
    public void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayers);
        SoundManager.PlaySound("MELEESTAB");

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);

            enemy.GetComponent<PlayerControls>().Die();


        }
    }
    //attackrange testaus
   /* void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }*/

    public IEnumerator Wait()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(2);
        Debug.Log("done");
    }

    public void Die()
    {
        Debug.Log("died");
        dead = true;
        animator.SetTrigger("Die");
        eyes.gameObject.SetActive(false);
        light.gameObject.SetActive(false);

        GetComponent<StateMachine>().enabled = false;
        
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
