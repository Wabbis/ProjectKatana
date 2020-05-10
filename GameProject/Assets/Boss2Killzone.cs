using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Killzone : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject gameplayCamera;
    public GameObject deathCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerControls sn = player.GetComponent<PlayerControls>();
            sn.canTakeDamage = true;
            collision.GetComponent<PlayerControls>().Die();

            gameplayCamera.SetActive(false);
            deathCamera.SetActive(true);
            boss.GetComponent<Boss2>().SetPlayerDead(true);
        }
    }
}
