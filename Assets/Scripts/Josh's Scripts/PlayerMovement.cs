using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   //player movement speed
    private float movementSpeed;
    private Vector2 moveInput;

    private void Awake()
    {
        movementSpeed = GetComponent<PlayerStats>().moveSpeed;
    }

    /// <summary>
    /// Player movement with Unity's input manager system - AHL (3/1/21)
    /// </summary>
    public void playerMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {


    }

    private void Update()
    {
        transform.position += new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * movementSpeed;
        /*//If Player Hits W
        if (Input.GetKey(KeyCode.W))
        {
            //Move Player Forward
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }

        //If Player Hits A
        if (Input.GetKey(KeyCode.A))
        {
            //Move Player Left
            transform.position += -transform.right * Time.deltaTime * movementSpeed;
        }

        //If Player Hits D
        if (Input.GetKey(KeyCode.D))
        {
            //Move Player Right
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }

        //If Player Hits S
        if (Input.GetKey(KeyCode.S))
        {
            //Move Player Back
            transform.position += -transform.up * Time.deltaTime * movementSpeed;
        }*/
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }


}

