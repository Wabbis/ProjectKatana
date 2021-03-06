﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component2 : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject cutscene;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            levelManager.GetComponent<Level4Script>().AddComponent(2);
            SoundManager.PlaySound("PICKUP");
            cutscene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
