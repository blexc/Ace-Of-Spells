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
    /// AHL (2/15/21) - Function that takes in the card that was used and then updates it based off the deck pull
    /// </summary>
    private void cardUpdate(GameObject card)
    {
        card.GetComponent<CardDisplay>().card = cardsInHand[0];
       //     Find("Name").GetComponent<Text>() = card.name;
        //elementText.text = card.element;
        //desctiptionText.text = card.description;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardsInHand = deck.GetComponent<Deck>().Hand;
        cardUpdate(card1);
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
