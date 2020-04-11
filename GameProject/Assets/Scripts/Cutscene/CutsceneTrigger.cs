using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;           // Mikä timeline toistetaan?
    public bool stopTimeDebug;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.CompareTag("Player"))
        {
            if (stopTimeDebug)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;

            timeline.Play();
        }
    }
}
