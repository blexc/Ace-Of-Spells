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

        if (moveInput.y > 0.5f && !animator.GetBool("side") && !animator.GetBool("sidealt"))
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

  
    
}

 
            


