using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    public Transform[] leftPositions;
    public Transform[] rightPositions;
    public PlayerControls player;
    public GameObject projectile;
    public GameObject shield;
    public Image indicator;

    public float baseSpeed;
    public float speed;
    public float dashSlow;
    public int dashes;
    public float projectileSpeed;
    private bool start=true;

    public bool hit;
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
        indicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (dashes > 0)
        {
            start = false;
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
            if (!start)
            {
                if (!hit)
                {
                    shield.gameObject.SetActive(false);
                    vulnerable = true;
                }
                indicator.gameObject.SetActive(true);
                indicator.fillAmount = timeLeft / vulnerTimer;

            }        

            timeLeft -= Time.deltaTime;
        

            if (timeLeft < 0)
            {
                indicator.gameObject.SetActive(false);
                shield.gameObject.SetActive(true);
                dashes = 3;
                timeLeft = vulnerTimer;
                vulnerable = false;
                hit = false;
            }


        }


    }
    public void Shoot()
    {
        GameObject newBullet = Object.Instantiate(projectile, transform.position, Quaternion.identity);
        //  newBullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
        newBullet.GetComponent<BossProjectile>().dir = (player.transform.position - transform.position).normalized * projectileSpeed * Time.deltaTime;
        Destroy(newBullet.gameObject, 5);
    }


    public void TakeDamage()
    {
        //  health--;
        Debug.Log("dmg taken");
        vulnerable = false;
        shield.gameObject.SetActive(true);
        hit = true;
        /* if (health == 0)
         {
             Die();
         }*/
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
