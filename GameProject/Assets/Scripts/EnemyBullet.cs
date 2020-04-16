using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Player")
        {
            // Destroy(collision.gameObject);
            Debug.Log("Player hit");
        }
        
        
            Destroy(this.gameObject);
        
        
    }

 
}
