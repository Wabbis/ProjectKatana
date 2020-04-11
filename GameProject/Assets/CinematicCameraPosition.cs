using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraPosition : MonoBehaviour
{
    public GameObject gameplayCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCameraPosition()
    {
        gameObject.transform.position = gameplayCamera.transform.position;
    }
}
