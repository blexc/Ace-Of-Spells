using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    //Variable List
    public Card locked, unlocked; //Card variables to hold the locked card and unlocked card aspects that will be used


    /// <summary>
    /// Function that sets the cards image when the player reaches this menu based on if they own the card or not - AHL (3/23/21)
    /// </summary>
    private void OnEnable()
    {
        if(!unlocked.isObtained)
        {
            GetComponentInChildren<CardDisplay>().card = locked;
        }

        else
            GetComponentInChildren<CardDisplay>().card = unlocked;
    }
}
