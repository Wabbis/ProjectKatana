using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player died in goop");
            collision.GetComponent<PlayerControls>().Die();
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "foreground";
            collision.GetComponent<SpriteRenderer>().sortingOrder = -1;
           
        }

    }
   
}
