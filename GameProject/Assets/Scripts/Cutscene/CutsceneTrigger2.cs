using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger2 : MonoBehaviour
{
    public GameObject cutscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            cutscene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
