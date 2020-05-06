using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private void FixedUpdate()
    {
       /* transform.localScale = transform.localScale * 0.85f;
        if (transform.localScale.x < 0.001f)
        {
            Destroy(gameObject);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player blown up");
        }
    }

}
