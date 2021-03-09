using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //player movement speed
    private float movementSpeed;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        movementSpeed = GetComponent<PlayerStats>().moveSpeed;
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        

    }

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
        }
    }


}

