using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscardCard : MonoBehaviour
{
    //Variable Initialization/Declaration List
    public Pause pauseScript;

    /// <summary>
    /// Function the Card buttons will access for the player to decide if this is the card they want to discard or not - AHL (4/14/21)
    /// </summary>
    public void DiscardThisCard()
    { 
        if (pauseScript.canDestroy)
        {
            pauseScript.DiscardCardFinalPopup.SetActive(true);
            //DiscardCardFinalText.text = ("Are you sure you want to discard " + name + '?');
        }

        else
            print("No Destroying today bud");
    }
}
