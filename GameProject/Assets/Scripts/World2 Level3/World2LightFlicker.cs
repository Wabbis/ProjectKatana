using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class World2LightFlicker : MonoBehaviour
{
    public Light2D buildingLight;
    public bool lightOn;
    public float intensity;

    public float randomStartingTime;
    public float timer;

    public float lightOnTime;


    // Start is called before the first frame update
    void Start()
    {
        buildingLight = gameObject.GetComponent<Light2D>();
        lightOn = false;
        randomStartingTime = Random.Range(0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < randomStartingTime && !lightOn)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= randomStartingTime && !lightOn)
        {
            lightOn = true;
            buildingLight.intensity = intensity;
            randomStartingTime = 3;
            timer = 0;
        }

        if (timer < lightOnTime && lightOn)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= lightOnTime && lightOn)
        {
            lightOn = false;
            buildingLight.intensity = 0;
            timer = 0;
        }
    }
}
