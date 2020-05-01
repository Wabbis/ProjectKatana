using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    PlayerControls player;
    public bool above;
   
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        gameObject.layer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            if (!above)
            {
                gameObject.layer = 13;
                above = true;
            }
        }
        else
        {
            if (above)
            {
                gameObject.layer = 2;
                above = false;
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButton("Down"))
            {

                gameObject.layer = 2;
            }
        }
    }

}
