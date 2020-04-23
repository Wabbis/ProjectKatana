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
    public bool block;
    public int maxJumps;
    private int jumpsLeft;

    //Player Statistics
    public float playerSpeed;
    public float jumpForce;
    public float dashForce;
    public float airDragMultiplier;

    //Attacks
    public bool canAttack;
    public bool canCounter;
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    public float counterCooldown;


    //Used for translating Controls to Movement (Update -> FixedUpdate) 
    private float movDirTemp;
    private bool jumpTemp;
    private bool counterTemp;
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
        canCounter = true;
        block = false;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            counterTemp = true;
        }
        if (Input.GetButton("Fire3"))
        {
            block = true;
            Debug.Log("Blocking");
        }
        else
        {
            block = false;
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
                    animator.SetTrigger("Landed");
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

        if (counterTemp && canCounter) { Counter(); }

    }

    //Makes the player Jump
    private void Jump()
    {
        animator.SetTrigger("Jump");
        playerRB.velocity = Vector2.up * jumpForce;
        jumpsLeft--;
    }

    //Moves the player
    private void Counter()
    {
        Debug.Log("Counter");
        animator.SetTrigger("Counter");
        playerRB.velocity = gameObject.transform.right * dashForce;
        canCounter = false;
    }

    //Resets temporary variables
    private void ResetTemp()
    {
        movDirTemp = 0;
        jumpTemp = false;
        crouchTemp = false;
        attackTemp = false;
        counterTemp = false;
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
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
            //Gets array of hit targets
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            

                if (hitEnemies.Length > 0)
                {
                    
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("Hit: " + enemy.name);


                        //TÄHÄN FUNKITIO JOKA TEKEE VIHOLLISEEN VAHINKOA
                        


                    }
                }



            canAttack = false;
            
            StartCoroutine(Cooldown(attackCooldown));
        }
    }

    
    public IEnumerator Cooldown(float duration)
    {
        
        yield return new WaitForSeconds(duration);
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
        if(playerRB.velocity.y < -0.5)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

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
