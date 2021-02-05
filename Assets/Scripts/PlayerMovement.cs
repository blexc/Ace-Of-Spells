﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //If Player Hits W
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
        }

    }
}

