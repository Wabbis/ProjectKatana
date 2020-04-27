using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLaserOff : MonoBehaviour
{
    public GameObject activate1;
    public GameObject activate2;
    public GameObject disActivate1;
    public GameObject disActivate2;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        this.gameObject.SetActive(false);
        if (activate1 != null) { activate1.SetActive(true); }
        if (activate2 != null) { activate2.SetActive(true); }
        if (disActivate1 != null) { disActivate1.SetActive(false); }
        if (disActivate2 != null) { disActivate2.SetActive(false); }
        

    }
}
