using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uccoActivate : MonoBehaviour
    
{
    public GameObject ucco;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateUcco());
    }

    // Update is called once per frame
    IEnumerator ActivateUcco()
    {
        yield return new WaitForSeconds(0.75f);
        ucco.SetActive(true);
    }
}
