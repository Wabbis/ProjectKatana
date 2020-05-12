using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinProjectile : MonoBehaviour
{
    public int speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
