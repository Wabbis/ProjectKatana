using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneTrigger : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject platforms;
    public GameObject levelManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cutscene.SetActive(true);
            platforms.SetActive(false);
            levelManager.GetComponent<World1Level1Script>().SetTrapActivated(true);
            gameObject.SetActive(false);
        }
    }
}
