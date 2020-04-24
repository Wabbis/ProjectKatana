using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicComponentHover : MonoBehaviour
{
    public float height;

    private float hoverTime;
    public float hoverMaxTime;

    public bool decending;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hoverTime += Time.deltaTime;

        if (hoverTime >= hoverMaxTime)
        {
            hoverTime = 0;
            decending = !decending;
        }

        float lerpTime = hoverTime / hoverMaxTime;

        if (decending)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y + height, lerpTime), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y - height, lerpTime), transform.position.z);
        }
    }
}
