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
            // if the object is a door, then go interact with that
            var door = interactableObject.GetComponent<Door>();
            if (door) door.Open();

            // otherwise, do something else....
            var chest = interactableObject.GetComponent<Reward>();
            if (chest) chest.RecieveReward();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag == "Interactable")
       {
            canInteract = true;
            interactableObject = other.gameObject;

            var door = interactableObject.GetComponent<Door>();
            if (door) door.IndicateActive();
       }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            var door = interactableObject.GetComponent<Door>();
            if (door) door.IndicateStop();

            canInteract = false;
            interactableObject = null;
        }
    }






}
