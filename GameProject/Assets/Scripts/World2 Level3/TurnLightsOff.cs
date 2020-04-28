using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLightsOff : MonoBehaviour
{
    public GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            lights.SetActive(false);
    }
}
