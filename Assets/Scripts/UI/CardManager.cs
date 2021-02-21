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
    public int selectedCard = 1; //Variable to keep track of which card is selected in hand and always starts with the 1st card
    private Vector3 maxScale = new Vector3(1f, 1f, 1f); //Vector to hold the maximum scale for the selected card in hand
    private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); //Vector to hold the maximum scale for the selected card in hand


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

    /// <summary>
    /// AHL (2/21/21) - Function that updates the cards UI based on what is selected
    /// </summary>
    public void showSelectedCard(int cardCurr)
    {
        if(cardCurr == 1) //First card is selected
            card1.transform.localScale = maxScale;
        else if(cardCurr == 2) //Second card is selected
            card2.transform.localScale = maxScale;
        else //Third card is selected
            card3.transform.localScale = maxScale;
    }

    /// <summary>
    /// AHL (2/21/21) - Function that updates the cards UI based on what was selected
    /// </summary>
    public void UnSelectCard(int cardPast)
    {
        if (cardPast == 1) //First card is selected
            card1.transform.localScale = minScale;
        else if (cardPast == 2) //Second card is selected
            card2.transform.localScale = minScale;
        else //Third card is selected
            card3.transform.localScale = minScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardUpdate();
        showSelectedCard(selectedCard);
    }

    /// <summary>
    /// AHL (2/21/21) - Update that is called once per frame
    /// If certain keys are pushed then the selected card in hand changes
    /// </summary>
    void Update()
    {
        //Moves over 1 card to the left
        if (Input.GetKeyDown(KeyCode.Z))
        {
            int previous = selectedCard; //Variable to hold the past card that was selected

            if (selectedCard == 1)
                selectedCard = 3;
            else
                selectedCard--;

            UnSelectCard(previous);
            showSelectedCard(selectedCard);
        }

        //Moves over 1 card to the right
        if (Input.GetKeyDown(KeyCode.X))
        {
            int previous = selectedCard; //Variable to hold the past card that was selected

            if (selectedCard == 3)
                selectedCard = 1;
            else
                selectedCard++;

            UnSelectCard(previous);
            showSelectedCard(selectedCard);
        }
    }
}
