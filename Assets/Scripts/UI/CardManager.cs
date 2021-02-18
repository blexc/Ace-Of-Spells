using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    //Variable Initalization/Declaration
    public GameObject card1, card2, card3; //Card variables to get the card templates from the deck.
    public GameObject deck; //Deck variable to hold the deck prefab
    public TMP_Text deckUI; //Deck # UI variable to adjust that number as needed
    public TMP_Text discardUI; //Discard # UI variable to adjust that number as needed
    List<Card> cardsInHand;

    /// <summary>
    /// AHL (2/17/21) - Function that updates the cards UI based off the deck pull
    /// </summary>
    public void cardUpdate()
    {
        cardsInHand = deck.GetComponent<Deck>().Hand;
        card1.GetComponent<CardDisplay>().card = cardsInHand[0];
        card2.GetComponent<CardDisplay>().card = cardsInHand[1];
        card3.GetComponent<CardDisplay>().card = cardsInHand[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        cardUpdate();
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
