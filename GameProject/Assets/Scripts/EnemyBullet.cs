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
    public Vector2 velo;
    


    private void Start()
    {
        lt = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        rb = GetComponent<Rigidbody2D>();
        velo = rb.velocity;
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
        rb.velocity = -velo;
        ChangeColor();
        ChangeLayer();
        counter = true;
     //   Debug.Log("reflect");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Environment")
        {
            Destroy(this.gameObject);

        }
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {
                
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
        if (collision.transform.tag == "Player")
        {
            
         // Reflect();
            // Destroy(collision.gameObject);
        //    Debug.Log("Player hit");
        }
        
        
           // Destroy(this.gameObject);
        
        
    }

 
}
