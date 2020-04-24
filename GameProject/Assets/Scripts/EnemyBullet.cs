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
    Rigidbody2D rb;
    

    private void Start()
    {
        lt = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        
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
        rb.velocity *= -1;
        ChangeColor();
        ChangeLayer();
        counter = true;
        Debug.Log("reflect");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);

        }
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {
                
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
                Destroy(gameObject);
            }
        }
        if (collision.transform.tag == "Player")
        {
            
         // Reflect();
            // Destroy(collision.gameObject);
        //    Debug.Log("Player hit");
        }


        // Destroy(this.gameObject);

        if (collision.transform.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerControls>().block)
            {
                Reflect();
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);

        }
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {

                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
                Destroy(gameObject);
            }
        }
        if (collision.transform.tag == "Player")
        {

            // Reflect();
            // Destroy(collision.gameObject);
            //    Debug.Log("Player hit");
        }


        // Destroy(this.gameObject);

        if (collision.transform.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerControls>().block)
            {
                Reflect();
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

}
