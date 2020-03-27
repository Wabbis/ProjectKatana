using System.Collections;
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
    public const float checkRadius = 0.02f;
    public bool grounded;

    //Player Statistics
    public float playerSpeed;
    public float jumpForce;
    public float airDragMultiplier;

    //Used for translating Controls to Movement (Update -> FixedUpdate) 
    private float movDirTemp;
    private bool jumpTemp;
    private bool crouchTemp;
    private Vector2 currentVelocity;


    private bool cSwitch = true;

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
        jumpTemp = Input.GetButtonDown("Jump");
        crouchTemp = Input.GetKeyDown(KeyCode.X);
    }

    private void FixedUpdate()
    {
        CheckGround();
        Movement();
        ResetTemp();
    }


    private void CheckGround()
    {
        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius);
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

        if (crouchTemp)
        {
            if (cSwitch)
            {
                playerCollider.enabled = false;
                cSwitch = false;
            }
            else
            {
                playerCollider.enabled = true;
                cSwitch = true;
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
}
