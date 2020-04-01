using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D playerRB;

    public Image filler; 


    public float counter;
    public float maxCounter; 




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime,
            0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);


        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(counter > maxCounter)
        {
            
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        

    }
}

    
