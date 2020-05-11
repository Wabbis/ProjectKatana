﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePlayerCharacter()
    {
        player.transform.position = newPosition;
        if(player.transform.rotation.eulerAngles.y != 0)
        {
            player.transform.Rotate(0, -180, 0);
        }
    }
}
