using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }

 
}
