﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlaceHolder : MonoBehaviour
{
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ded");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
