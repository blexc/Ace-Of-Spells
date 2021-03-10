using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   //player movement speed
    private float movementSpeed;

    public Animator animator;
    public SpriteRenderer sprite;
    private Vector2 moveInput;


    private void Awake()
    {
        movementSpeed = GetComponent<PlayerStats>().moveSpeed;
    }

    /// <summary>
    /// Player movement with Unity's input manager system - AHL (3/1/21)
    /// </summary>
    /// 
    
    public void playerMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    


    private void Update()
    {
        transform.position = new Vector2(transform.position.x + (moveInput.x * Time.deltaTime * movementSpeed), transform.position.y + (moveInput.y * Time.deltaTime * movementSpeed));

        if  (moveInput.x != 0 || moveInput.y !=0)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        

        if ((moveInput.x > 0.5f) && !animator.GetBool("front"))
        {
            animator.SetBool("side", true);
            animator.SetBool("sidealt", false);
        }
        else if ((moveInput.x < -0.5f) && !animator.GetBool("front"))
        {
            animator.SetBool("sidealt", true);
            animator.SetBool("side", false);
        }
        else
        {
            animator.SetBool("side", false);
            animator.SetBool("sidealt", false);
        }

        if (moveInput.y > 0.5f && !animator.GetBool("side"))
        {
            animator.SetBool("back", true);
            animator.SetBool("front", false);
        }
        else if (moveInput.y < -0.5f)
        {
            animator.SetBool("front", true);
            animator.SetBool("back", false);
        }
        else
        {
            animator.SetBool("front", false);
            animator.SetBool("back", false);
        }

        if (moveInput.x == 0f && moveInput.y ==0f)
        {
            animator.SetBool("idle", true);
        }
        else
        {
            animator.SetBool("idle", false);
        }

    }

    /*
    private void Update()
    {
        //If Player Hits W
        if (Input.GetKey(KeyCode.W))
        {
            //Move Player Forward
            transform.position += transform.up * Time.deltaTime * movementSpeed;
            animator.SetBool("back", true);
        }
        else
        {
            animator.SetBool("back", false);
        }



        //If Player Hits A
        if (Input.GetKey(KeyCode.A))
        {
            //Move Player Left
            transform.position += -transform.right * Time.deltaTime * movementSpeed;

            animator.SetBool("side", true);
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        //If Player Hits D
        if (Input.GetKey(KeyCode.D))
        {
            //Move Player Right
            transform.position += transform.right * Time.deltaTime * movementSpeed;

            animator.SetBool("side", true);
        }

        //set side animator bool to false if neither key are being pressed.
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            animator.SetBool("side", false);
        }
        transform.position += new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * movementSpeed;
        
        //If Player Hits S
        if (Input.GetKey(KeyCode.S))
        {
            //Move Player Back
            transform.position += -transform.up * Time.deltaTime * movementSpeed;
            animator.SetBool("front", true);
        }
        else
        {
            animator.SetBool("front", false);
        }
        

        if (!Input.GetKey(KeyCode.S) && 
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.W))
        {
            animator.SetBool("moving", false);
        
        }
        else
        {
            animator.SetBool("moving", true);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    */
    
}

 
            


