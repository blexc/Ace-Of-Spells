using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoriteCard : MonoBehaviour
{
    //Variable Initialization/Declaration List
    public Pause pauseScript;

    /// <summary>
    /// Function to Favorite the selected card when the Card button is pressed and if it can't be destroyed - AHL (4/14/21)
    /// </summary>
    public void FavoriteSelectedCard()
    {
        //If the card isn't favorited then bring up the favorite pop-up
        if (!pauseScript.canDestroy && !GetComponent<CardDisplay>().card.isFavorite)
        {
            pauseScript.MakeFavoritePopup.SetActive(true);
            pauseScript.FavoriteCardText.text = ("Do you want to favorite " + GetComponent<CardDisplay>().card.name + '?');
        }

        //If the card is favorited then bring up the remove pop-up
        else if (!pauseScript.canDestroy && GetComponent<CardDisplay>().card.isFavorite)
        {
            pauseScript.RemoveFavoritePopup.SetActive(true);
            pauseScript.UnfavoriteCardText.text = ("Do you want to remove the favorite from " + GetComponent<CardDisplay>().card.name + '?');
        }
    }
}
