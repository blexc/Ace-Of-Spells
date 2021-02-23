using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool canInteract;

    public GameObject interactableObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Debug.Log(interactableObject);

            //Do something
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag == "Interactable")
       {
            canInteract = true;
            interactableObject = other.gameObject;
       }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            canInteract = false;
            interactableObject = null;

        }
    }






}
