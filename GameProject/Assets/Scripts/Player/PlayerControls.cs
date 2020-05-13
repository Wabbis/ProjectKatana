using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControls : MonoBehaviour
{   

    public GameObject player;
    public Animator animator;
    public Rigidbody2D playerRB;
    public Collider2D playerCollider;

    public Transform groundCheck;
    public Transform attackPoint;

    public LayerMask groundLayers;
    public LayerMask enemyLayers;

    public GameManager gameManager;
    public LevelManager levelManager;
    public PauseMenu pauseMenu;
    public GameObject light;
   


    public float groundCheckRadius = 0.59f;
    public bool grounded;
    public bool canTakeDamage;
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
    public bool deflecting;
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




    private void Start()
    {
        //
        gameManager = FindObjectOfType<GameManager>();
        gameManager.player = gameObject;
        gameManager.playerAlive = true;
        levelManager = FindObjectOfType<LevelManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        //
        playerRB.isKinematic = false;
        playerCollider.enabled = true;
        canAttack = true;
        canCounter = true;
        dead = false;
        canTakeDamage = true;
        deflecting = false;
        jumpsLeft = maxJumps;
        //
        CheckLevel();
        SetControl(true);
        DisableDeathPanel();
    }


    private void Update()
    {
        if (gameManager.acceptPlayerInput)
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
        }

        //Testausta varten

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    SetCounterDeflect(!GetCounterDeflect()); 
        //}
        //if (Input.GetKeyDown(KeyCode.Y)) // KYS Bind
        //{
        //    if (!dead)
        //    {
        //        Die();
        //    }               
        //}
       
    }

    private void FixedUpdate()
    {
        CheckGround();
        Movement();
        Attacking();
        UpdateAnimations();
        ResetTemp();
    }


    /*
    -----------------------------------------------
    |                                             |
    |                  UTILITIES                  |
    |                                             |
    -----------------------------------------------
    */


    //Getter and Setter for player controls
    public bool GetControl() { return gameManager.acceptPlayerInput; }

    public void SetControl(bool state)
    {
        gameManager.acceptPlayerInput = state;

        if (!state)
        {
            playerRB.velocity = new Vector2(0, 0);
        }
    }

    // Getter and Setter for Improved Counter
    public bool GetCounterDeflect() { return improvedCounter; }
    public void SetCounterDeflect(bool state)
    { improvedCounter = state; }


    //Getter for Player Dead
    public bool getDead() { return dead; }


    // Improves the Players Counter ability after the Boss is defeated
    private void CheckLevel()
    {
        if (levelManager.currentLevel >= 8)
        {
            SetCounterDeflect(true);
        }
        else
        {
            SetCounterDeflect(false);
        }
    }

    // Checks if the player is standing on the ground
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


    // Sets Panel on (called from Player_Death animation)
    public void EnableDeathPanel() { pauseMenu.ToggleDeathPanel(true); }
    public void DisableDeathPanel() { pauseMenu.ToggleDeathPanel(false); }

    // Kills the player if they can take damage
    public void Die()
    {
        if (canTakeDamage == true && !dead)
        {
            dead = true;
            SetControl(false);
            light.SetActive(false);
            animator.SetTrigger("Dead");
            SoundManager.PlaySound("DEATHOOF");
            gameManager.PlayerDied();

            if(playerRB.IsSleeping())
            {
                playerCollider.enabled = false;
                playerRB.isKinematic = true;
            }
            else
            {
                // Check if player is moving/in the air - Slower to respond but ensures that player doesn't stay hovering in the air
                StartCoroutine(DeadAndStoppedMoving());
            }
        }
    }

    public IEnumerator DeadAndStoppedMoving()
    {
        //Wait till player's rigidbody is sleeping and then turn off collider and stop rigidbody

        while(!playerRB.IsSleeping())
        {
            yield return null;
        }

        playerCollider.enabled = false;
        playerRB.isKinematic = true;
        yield return null;
    }


    /*
    -----------------------------------------------
    |                                             |
    |             MOVEMENT HANDLING               |
    |                                             |
    -----------------------------------------------
    */

    // Moves player according to inputs
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
        SoundManager.PlaySound("DODGE");
         StartCoroutine(Invunerable(1.5f));
        playerRB.velocity = gameObject.transform.right * dashForce;
        canCounter = false;
        light.SetActive(false);

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

    // Checks an area around Attack-point and Deals damage to them
    private void Attacking()
    {
        if (attackTemp && canAttack)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
            SoundManager.PlaySound("SWORD");
            //Gets array of hit targets
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            

                if (hitEnemies.Length > 0)
                {
                    
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("Hit: " + enemy.name);

                        enemy.GetComponent<EnemyHealth>().TakeDamage();
                    }
                }
            canAttack = false;
            
            StartCoroutine(Cooldown(attackCooldown));
        }
    }

    // Sets Cooldown for player abilities
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

            if(dead == false) { light.SetActive(true); }
            
        }
    
    }

    // Makes the player Invunerable for a duration and Deflects bullets if the Player has the Improved Counter 
    public IEnumerator Invunerable(float seconds)
    {
        canTakeDamage = false;
        if (improvedCounter) { deflecting = true; }
        yield return new WaitForSeconds(seconds);
        canTakeDamage = true;
        deflecting = false;
    }


    /*
   -----------------------------------------------
   |                                             |
   |             ANIMATIONS                      |
   |                                             |
   -----------------------------------------------
   */

    // Updates the Animators parametres
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

    // Flips the player model to face the other way
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

    // Draws a visualization of ground checks and attacks radius (ONLY IN EDITOR)
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
