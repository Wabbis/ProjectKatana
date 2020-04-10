using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteeriScript : MonoBehaviour
{
    public GameObject mysteeriMies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mysteeriMies.SetActive(true);
    }
}
