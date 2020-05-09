using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerControls sn = player.GetComponent<PlayerControls>();
            sn.canTakeDamage = true;
            Debug.Log("Player died in goop");
            collision.GetComponent<PlayerControls>().Die();
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "foreground";
            collision.GetComponent<SpriteRenderer>().sortingOrder = -1;
           
        }

    }
   
}
