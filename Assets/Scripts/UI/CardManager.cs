using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
{
    //Variable Initalization/Declaration
    public GameObject card1, card2, card3; //Card variables to get the card templates from the deck.
    public GameObject deck; //Deck variable to hold the deck prefab
    public TMP_Text deckUI; //Deck # UI variable to adjust that number as needed
    public TMP_Text discardUI; //Discard # UI variable to adjust that number as needed
    List<Card> cardsInHand; //Connects with the CardsInHand List from the Deck script
    private Vector3 maxScale = new Vector3(1f, 1f, 1f); //Vector to hold the maximum scale for the selected card in hand
    private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); //Vector to hold the maximum scale for the selected card in hand


    /// <summary>
    /// AHL (2/17/21) - Function that updates the cards UI based off the deck pull
    /// </summary>
    public void cardUpdate()
    {
        cardsInHand = Deck.instance.Hand;
        card1.GetComponent<CardDisplay>().card = cardsInHand[0];
        card2.GetComponent<CardDisplay>().card = cardsInHand[1];
        card3.GetComponent<CardDisplay>().card = cardsInHand[2];
        discardUI.text = "" + Deck.instance.discardPile.Count; //Updates the discard Num UI
        deckUI.text = "" + Deck.instance.drawPile.Count; //Updates the deck Num UI
    }

    /// <summary>
    /// AHL (2/23/21) - Function that updates the cards UI based on what is selected
    /// </summary>
    public void showSelectedCard(int cardCurr)
    {
        if(cardCurr == 0) //First card is selected
        {
            card3.transform.localScale = minScale;
            card1.transform.localScale = maxScale;
        }
        else if(cardCurr == 1) //Second card is selected
        {
            card1.transform.localScale = minScale;
            card2.transform.localScale = maxScale;
        }  
        else //Third card is selected
        {
            card2.transform.localScale = minScale;
            card3.transform.localScale = maxScale;
        }
    }
}
