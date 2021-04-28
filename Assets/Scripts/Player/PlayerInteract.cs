using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public bool canInteract;

    public GameObject interactableObject;

    /// <summary>
    /// Interact function to interact with a door or item by using the 'Q' key - AHL(3/1/21)
    /// **This is based off the input manager so the key can be adjusted**
    /// </summary>
    public void interact(InputAction.CallbackContext context)
    {
        if (context.performed && canInteract)
        {
            // if the object is a door, then go interact with that
            var door = interactableObject.GetComponent<Door>();
            if (door) door.Open();

            // otherwise, do something else....
            var chest = interactableObject.GetComponent<Reward>();
            if (chest) chest.RecieveReward();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;
            interactableObject = other.gameObject;

            var door = interactableObject.GetComponent<Door>();
            if (door) door.IndicateActive();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") && interactableObject)
        {
            var door = interactableObject.GetComponent<Door>();
            if (door) door.IndicateStop();

            canInteract = false;
            interactableObject = null;
        }
    }
}
