using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Push : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    public bool push = false;
    public bool back = false;
    public int speed = 3;
    public int counter = 0;
    public GameObject palikka;
    public GameObject viewcone;

    // Start is called before the first frame update
    void Start()
    {
       // endPosition = (startPosition.x - 5, startPosition.y, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(push == true) {
            counter++;
            palikka.transform.Translate(Vector3.left * Time.deltaTime * speed);
            if(counter > 60)
            {
                push = false;
                counter = 0;
                back = true;

            }
        }
        if(back == true)
        {
            counter++;
            palikka.transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (counter > 60)
            {
                back = false;
                counter = 0;
                viewcone.GetComponent<SpriteRenderer>().enabled = false;
                viewcone.GetComponentInChildren<Light2D>().enabled = false;

            }
        }
    }
    public void SetPushTrue()
    {
        push = true;
    }
}


//transform.position = Vector3.Lerp(startPosition, endPosition, speed* Time.deltaTime);