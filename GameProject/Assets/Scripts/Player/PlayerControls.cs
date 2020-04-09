using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool hasControl = true;

    public GameObject player;
    public Animator animator;
    public Rigidbody2D playerRB;
    public Collider2D playerCollider;
    public Transform headCheck;
    public Transform groundCheck;
    public Transform attackPoint;
    public LayerMask groundLayers;
    public LayerMask enemyLayers;
    


    public float groundCheckRadius = 0.02f; //works well for scale 4 & 4
    public float headCheckRadius = 0.84f;    //works well for scale 4 & 4
    public bool grounded;
    public bool canCrouch;
    public int maxJumps;
    private int jumpsLeft;

    //Player Statistics
    public float playerSpeed;
    public float jumpForce;
    public float airDragMultiplier;

    //Attacks
    private bool canAttack;
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;


    //Used for translating Controls to Movement (Update -> FixedUpdate) 
    private float movDirTemp;
    private bool jumpTemp;
    private bool crouchTemp;
    private bool attackTemp;
    private bool facingRight = true;


    private bool isCrouching = false;

    //Getter and Setter for player controls
    public bool GetControl() { return hasControl; }
    public void SetControl(bool state) { hasControl = state; }



    private void Start()
    {
        canAttack = true;
        jumpsLeft = maxJumps;
    }


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
        if (Input.GetButtonDown("Fire1"))
        {
            attackTemp = true;
        }
    }


    private void FixedUpdate()
    {
        CheckGround();
        CheckHead();
        Movement();
        Attacking();
        UpdateAnimations();
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
                    jumpsLeft = maxJumps;
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
        if (facingRight)
        {
            transform.Translate(new Vector3(movDirTemp * playerSpeed * Time.deltaTime, 0, 0));
        }
        else
        {
            transform.Translate(new Vector3(movDirTemp * playerSpeed * Time.deltaTime * -1f, 0, 0));
        }
        
        if (jumpTemp && jumpsLeft > 0) { Jump(); }
    }

    //Makes the player Jump
    private void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsLeft--;
    }

    //Resets temporary variables
    private void ResetTemp()
    {
        movDirTemp = 0;
        jumpTemp = false;
        crouchTemp = false;
        attackTemp = false;
    }


    /*
   -----------------------------------------------
   |                                             |
   |             ATTACKS                         |
   |                                             |
   -----------------------------------------------
   */


    private void Attacking()
    {
        if (attackTemp && canAttack)
        {

            //Gets array of hit targets
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit: " + enemy.name);


                //TÄHÄN FUNKITIO JOKA TEKEE VIHOLLISEEN VAHINKOA


            }

            
            canAttack = false;
            Cooldown(attackCooldown);
        }
    }

    private IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }


    /*
   -----------------------------------------------
   |                                             |
   |             ANIMATIONS                      |
   |                                             |
   -----------------------------------------------
   */


    private void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(movDirTemp * playerSpeed));

        Flip();
    }


    private void Flip()
    {
        if (facingRight && movDirTemp < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
        else if (!facingRight && movDirTemp > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
    }




    /*
   -----------------------------------------------
   |                                             |
   |             DEBUG                           |
   |                                             |
   -----------------------------------------------
   */
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
