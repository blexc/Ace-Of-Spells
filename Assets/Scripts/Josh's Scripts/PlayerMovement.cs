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
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }


}

