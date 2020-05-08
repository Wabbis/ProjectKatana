using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    // Ehkä turha
    public bool hasControl = true;

    public GameObject gm; 
    public GameObject player;
    public Animator animator;
    public Rigidbody2D playerRB;
    public Transform groundCheck;
    public Transform attackPoint;
    public LayerMask groundLayers;
    public LayerMask enemyLayers;
    


    public float groundCheckRadius = 0.02f; //works well for scale 4 & 4
    public bool grounded;
    public bool canTakeDamage;
    public bool block;
    public int maxJumps;
    private int jumpsLeft;
    private bool dead;

    //Player Statistics
    public float playerSpeed;
    public float jumpForce;
    public float dashForce;
    public bool improvedCounter;
    

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
    private bool attackTemp;
    private bool facingRight = true;


   

    //Getter and Setter for player controls
    public bool GetControl() { return gm.GetComponent<GameManager>().acceptPlayerInput; }
    public void SetControl(bool state) 
    { gm.GetComponent<GameManager>().acceptPlayerInput = state; }


    //Getter and Setter for Improved Counter
    public bool GetCounterDeflect() { return improvedCounter; }
    public void SetCounterDeflect(bool state)
    { improvedCounter = state; }




    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManagement");
        gm.GetComponent<GameManager>().player = gameObject;
        canAttack = true;
        canCounter = true;
        block = false;
        dead = false;
        canTakeDamage = true;
        jumpsLeft = maxJumps;
        SetControl(true);
    }


    private void Update()
    {
        if (gm.GetComponent<GameManager>().acceptPlayerInput)
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
        if (Input.GetButtonDown("Fire1"))
        {
            attackTemp = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            counterTemp = true;
            block = true;

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            block = true;
            Debug.Log("Blocking");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetCounterDeflect(!GetCounterDeflect()); 
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!dead)
            {
                Die();
            }
            else
            {
                SetControl(true);
                animator.SetBool("IsDead", false);
            }
               
        }
        else
        {
            block = false;
        }
    }


    private void FixedUpdate()
    {
        CheckGround();
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


    public void Die()
    {

        if (canTakeDamage == true)
        {
            SetControl(false);
            animator.SetBool("IsDead", true);
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
        SoundManager.PlaySound("WHOOSH");
        playerRB.velocity = Vector2.up * jumpForce;
        jumpsLeft--;
    }




   

    //Moves the player
    private void Counter()
    {
        Debug.Log("Counter");
        animator.SetTrigger("Counter");

        if (improvedCounter) { StartCoroutine(Invunerable(1.5f)); }

        playerRB.velocity = gameObject.transform.right * dashForce;
        canCounter = false;

        StartCoroutine(Cooldown(counterCooldown));
    }

    //Resets temporary variables
    private void ResetTemp()
    {
        movDirTemp = 0;
        jumpTemp = false;
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
                    enemy.GetComponent<EnemyHealth>().TakeDamage();
                        


                    }
                }



            canAttack = false;
            
            StartCoroutine(Cooldown(attackCooldown));
        }
    }

    
    public IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(duration == attackCooldown)
        {
            canAttack = true;
        }
        else if(duration == counterCooldown)
        {
            canCounter = true;
        }
        
    }

    public IEnumerator Invunerable(float seconds)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(seconds);
        canTakeDamage = true;
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
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
