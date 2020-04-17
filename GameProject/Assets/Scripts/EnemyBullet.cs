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


    private void Start()
    {
        lt = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    public void ChangeLayer()
    {
        gameObject.layer = 0;
    }
    public void ChangeColor()
    {
        lt.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (counter)
        {
            if (collision.transform.tag == "Enemy")
            {
              
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
        if (collision.transform.tag == "Player")
        {
          
            // Destroy(collision.gameObject);
            Debug.Log("Player hit");
        }
        
        
            Destroy(this.gameObject);
        
        
    }

 
}
