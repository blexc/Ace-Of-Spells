using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    //Variable Initalization/Declaration
    public GameObject card1, card2, card3; //Card variables to get the card templates from the deck.
    public GameObject deck;
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
