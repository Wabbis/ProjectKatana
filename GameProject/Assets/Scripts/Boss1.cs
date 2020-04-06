using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform[] leftPositions;
    public Transform[] rightPositions;
    public PlayerControls player;
    public GameObject projectile;

    public float baseSpeed;
    public float speed;
    public float dashSlow;
    public int dashes;
    public float projectileSpeed;

    public bool vulnerable;
    public float vulnerTimer;
    public float timeLeft;
    int nextPos;
    public bool left;
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        timeLeft = vulnerTimer;
        nextPos = Random.Range(0, 4);
        player = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {

        if (dashes > 0)
        {
            speed -= dashSlow * Time.deltaTime;
            if (!left)
            {
                Transform next = leftPositions[nextPos];

                transform.position = Vector3.MoveTowards(transform.position, next.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, next.position) < 0.001f)
                {
                    left = true;
                    speed = baseSpeed;
                    nextPos = Random.Range(0, 4);
                    next = rightPositions[nextPos];
                    dashes--;
                    Shoot();


                }

            }
            else
            {
                Transform next = rightPositions[nextPos];

                transform.position = Vector3.MoveTowards(transform.position, next.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, next.position) < 0.001f)
                {
                    left = false;
                    speed = baseSpeed;
                    nextPos = Random.Range(0, 4);
                    next = leftPositions[nextPos];
                    dashes--;
                    Shoot();


                }

            }


        }
        else
        {
            vulnerable = true;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                dashes = 3;
                timeLeft = vulnerTimer;
                vulnerable = false;
            }


        }


    }
    public void Shoot()
    {
        GameObject newBullet = Object.Instantiate(projectile, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
    }


    void TakeDamage()
    {
        health--;
        vulnerable = false;
        if (health == 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
