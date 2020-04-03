using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExitLightFlicker : MonoBehaviour
{
    public GameObject flickeringLight;
    public float flickerTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while(true)
        {
            flickeringLight.SetActive(!flickeringLight.activeSelf);
            yield return new WaitForSeconds(flickerTime);
        }
    }
}
