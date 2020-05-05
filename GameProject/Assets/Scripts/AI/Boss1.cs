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
    public Animator animator;

  //  public float baseSpeed;
    public float speed;
   // public float dashSlow;
    public int dashes;
    int dashesLeft;
    public float projectileSpeed;
    private bool start=true;

    public bool dead;
    public bool hit;
    public bool vulnerable;
    public float vulnerTimer;
    public float timeLeft;
    int nextPos;
    public bool left;
    public int health;

    float attacTime = 1;
    float attackTimeLeft=1;


    // Start is called before the first frame update
    void Start()
    {
        dashesLeft = dashes;
      //  speed = baseSpeed;
        timeLeft = vulnerTimer;
        nextPos = Random.Range(0, 4);
        player = FindObjectOfType<PlayerControls>();
        indicator.gameObject.SetActive(false);
        animator = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {

            if (dashesLeft > 0)
            {
                start = false;
              //  speed -= dashSlow * Time.deltaTime;
                if (!left)
                {
                    Transform next = leftPositions[nextPos];

                    transform.position = Vector3.MoveTowards(transform.position, next.position, speed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, next.position) < 0.001f)
                    {
                        if (attackTimeLeft == 1)
                        {
                            Shoot();
                            Flip();
                        }
                      

                        attackTimeLeft -= Time.deltaTime;
                        if (attackTimeLeft < 0)
                        {
                            
                            left = true;
                          //  speed = baseSpeed;
                            nextPos = Random.Range(0, 4);
                            next = rightPositions[nextPos];
                            dashesLeft--;
                            attackTimeLeft = attacTime;
                        }               
                                             

                    }

                }
                else
                {
                    Transform next = rightPositions[nextPos];

                    transform.position = Vector3.MoveTowards(transform.position, next.position, speed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, next.position) < 0.001f)
                    {
                        if (attackTimeLeft == 1)
                        {
                            Flip();
                            Shoot();
                        }
                          

                        attackTimeLeft -= Time.deltaTime;
                        if (attackTimeLeft < 0)
                        {
                            
                            left = false;
                          //  speed = baseSpeed;
                            nextPos = Random.Range(0, 4);
                            next = leftPositions[nextPos];
                            dashesLeft--;
                            attackTimeLeft = attacTime;

                        }

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
                    dashesLeft = dashes;
                    timeLeft = vulnerTimer;
                    vulnerable = false;
                    hit = false;
                }


            }
        }


    }
    void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dashesLeft > 0)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("player hit");
                // kutsutaan pelaajan tappavaafunktiota
            }
        }
    }
    public void Counter()
    {
        animator.SetTrigger("Counter");
    }


    public void Shoot()
    {
        Debug.Log("shoot");
        animator.SetTrigger("Attack");
        GameObject newBullet = Object.Instantiate(projectile, transform.position, Quaternion.identity);
        //  newBullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
        newBullet.GetComponent<BossProjectile>().dir = (player.transform.position - transform.position).normalized * projectileSpeed;
        Destroy(newBullet.gameObject, 10);
    }


    public void TakeDamage()
    {
        dashes++;
        Debug.Log("dmg taken");
        vulnerable = false;
        shield.gameObject.SetActive(true);
        hit = true;
     
    }
    public void Die()
    {
        dead = true;
        animator.SetTrigger("Die");

       // Destroy(gameObject);
    }
}
