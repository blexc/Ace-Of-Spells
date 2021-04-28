using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed { get { return movementSpeed; } } // READONLY
    public float CurMoveSpeed { set { curMoveSpeed = value; } }

    //player movement speed
    [HideInInspector] float movementSpeed; // should not be changed in game 

    float curMoveSpeed;

    public Animator animator;
    public SpriteRenderer sprite;
    private Vector2 moveInput;
    Rigidbody2D rb;

    private void Awake()
    {
        movementSpeed = GetComponent<PlayerStats>().moveSpeed;
        curMoveSpeed = movementSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Player movement with Unity's input manager system - AHL (3/1/21)
    /// </summary>
    /// 
    public void playerMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// move player 
    /// </summary>
    private void FixedUpdate()
    {
        rb.velocity = moveInput * Time.fixedDeltaTime * curMoveSpeed;
    }
    
    /// <summary>
    /// adjust animations
    /// </summary>
    private void Update()
    {

        #region anims

        if (moveInput.x != 0 || moveInput.y !=0)
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

        #endregion
    }
}

 
            


