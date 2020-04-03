﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool hasControl = true;

    public GameObject player;
    public Rigidbody2D playerRB;
    public Collider2D playerCollider;
    public Transform headCheck;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public const float groundCheckRadius = 0.02f;
    public const float headCheckRadius = 1.0f;
    public bool grounded;
    public bool canCrouch;
    

    //Player Statistics
    public float playerSpeed;
    public float jumpForce;
    public float airDragMultiplier;

    //Used for translating Controls to Movement (Update -> FixedUpdate) 
    private float movDirTemp;
    private bool jumpTemp;
    private bool crouchTemp;
    private Vector2 currentVelocity;


    private bool isCrouching = false;

    //Getter and Setter for player controls
    public bool GetControl() { return hasControl; }
    public void SetControl(bool state) { hasControl = state; }



    private void Update()
    {
        if (hasControl)
        {
            Controls();
        }
    }

    //Gets input from player
    private void Controls()
    {
        movDirTemp = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            jumpTemp = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            crouchTemp = true;
        }
    }


    private void FixedUpdate()
    {
        CheckGround();
        CheckHead();
        Movement();
        ResetTemp();
    }

    //Checks if the player is standing on the ground
    private void CheckGround()
    {
        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    Debug.Log("Landed");
                }
            }
        } 
    }

    //Checks if the player can stand up
    private void CheckHead()
    {
        canCrouch = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(headCheck.position, headCheckRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                canCrouch = false;
                //Debug.Log("Head");
            }
        }
    }

    /*
    -----------------------------------------------
    |                                             |
    |             MOVEMENT HANDLING               |
    |                                             |
    -----------------------------------------------
    */


    //Moves player according to inputs
    private void Movement()
    {

        float velocity = movDirTemp * playerSpeed;


        //Slows down the player while in air
        if (!grounded)
        {
            velocity *= airDragMultiplier;
        }


        //Enables and Disables collider whenever player is crouching
        if (crouchTemp)
        {
            if (!isCrouching)
            {
                playerCollider.enabled = false;
                isCrouching = true;
            }
            else
            {
                if (canCrouch)
                {
                    playerCollider.enabled = true;
                    isCrouching = false;
                }  
            }
        }

        //transform.Translate(new Vector3(movDirTemp * playerSpeed * Time.deltaTime, 0, 0));
        playerRB.velocity = Vector2.SmoothDamp(playerRB.velocity, new Vector2(velocity, playerRB.velocity.y), ref currentVelocity, 0.22f);

        if (jumpTemp && grounded) { Jump(); }
    }

    //Makes the player Jump
    private void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //Resets temporary variables
    private void ResetTemp()
    {
        movDirTemp = 0;
        jumpTemp = false;
        crouchTemp = false;
    }







    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}