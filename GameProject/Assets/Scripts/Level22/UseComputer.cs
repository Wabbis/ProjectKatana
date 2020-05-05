using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseComputer : MonoBehaviour
{
    public bool canHack = false;
    public bool dwindle = true;
    public bool activate = false;
    public bool cutsceneStarted;
    public Slider hackBar;

    public float value = 0;
    public float gainPerMash;
    public int dwindleSpeed;
    public GameObject placeHolderThing;
    public GameObject placeHolderThing2;
    public GameObject lightRed;
    public GameObject lightGreen;

    // Start is called before the first frame update
    void Start()
    {
        hackBar.value = 0.0f;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        canHack = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        canHack = false;
    }
    void Update()
    {
        if(canHack == true)
        {
            if (Input.GetKeyDown("e"))
            {
                value += gainPerMash;
                hackBar.value = value;
            }
        }
        if(value < 0.0f)
        {
            value = 0.0f;
        }
        if(value >= 1.00f)
        {
            dwindle = false;
            activate = true;
        }

        
    }
    void FixedUpdate()
    {
        if(value > 0)
        {
            if((value - 0.01f) > 0.0f && dwindle == true)
            {
                value = value - (0.0001f * dwindleSpeed);
                hackBar.value = value;
            }
        }
        if(activate == true)
        {
            lightRed.SetActive(false);
            lightGreen.SetActive(true);
            placeHolderThing.SetActive(false);

            //start cutscene
            if (placeHolderThing2 != null && !cutsceneStarted)
            {
                cutsceneStarted = true;
                placeHolderThing2.SetActive(true);
            }
        }
    }
}
