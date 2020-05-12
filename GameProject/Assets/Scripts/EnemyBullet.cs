using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class EnemyBullet : MonoBehaviour
{
    public bool counter;
    // public LayerMask layer;
    public Color color;
    public UnityEngine.Experimental.Rendering.Universal.Light2D lt;
    public float speed;
    public Vector2 dir;
    public GameManager gameManager;
    

    private void Start()
    {
        lt = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        gameManager = FindObjectOfType<GameManager>();
        
    }
    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
    public void ChangeLayer()
    {
        gameObject.layer = 16;
    }
    public void ChangeColor()
    {
        lt.color = color;
    }
    public void Reflect()
    {

        if (!counter)
        {
            dir = -dir;
            counter = true;
            ChangeLayer();
            Debug.Log("reflect");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Environment")
        {
            Debug.Log("Hit a wall");
            //Destroy(this.gameObject);

        }
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Took Damage");
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
                Destroy(gameObject);
            }
        }
      
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Bullet hit Player");
            if (!collision.gameObject.GetComponent<PlayerControls>().deflecting)
            {
                Reflect();
                Debug.Log("Reflected");
            }
            else
            {
                Debug.Log("Player Died");
                //   Destroy(collision.gameObject);
                collision.gameObject.GetComponent<PlayerControls>().Die();
                Destroy(gameObject);
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Environment")
        {
            Debug.Log("Hit wall");
            //Destroy(this.gameObject);

        }
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Took Damage");
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
                Destroy(gameObject);
            }
        }


        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit Player again");

            if (collision.gameObject.GetComponent<PlayerControls>().deflecting)
            {
                Debug.Log("Reflected");
                Reflect();
            }
            else
            {
                Debug.Log("Player died");
                collision.GetComponent<PlayerControls>().Die();
                Destroy(gameObject);
            }
        }
    }

}
