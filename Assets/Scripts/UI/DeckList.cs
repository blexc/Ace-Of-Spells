using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DeckList : MonoBehaviour
{
    [SerializeField]
    private GameObject cardTemplate = null; //Variable to hold and create the cards for the deckList screen

    public List<Card> allCards; //A list of all the cards currently in the Discard, Hand, and Draw piles
    public Deck deck; //The deck script to keep track of the lists for the different card piles 

    [HideInInspector] public List<GameObject> templates; //List of the card templates created so they can be easily removed when the player leaves the scene

    //Function is called when the UI becomes active - AHL (4/12/21)
    private void OnEnable()
    {
        GetComponentInChildren<Scrollbar>().value = 1; //Resets the scrollbar to the top of the page for the player
        ShowSortedList();
    }

    //Function to remove all the cards 
    private void OnDisable()
    {
        refreshList();
    }

    //Custom function to go through adding cards to the list and sorting it to be displayed
    private void ShowSortedList()
    {
        foreach (Card card in deck.drawPile)
        {
            allCards.Add(card); //Adds the cards to the allCards list
        }

        foreach (Card card in deck.Hand)
        {
            allCards.Add(card); //Adds the cards to the allCards list
        }

        foreach (Card card in deck.discardPile)
        {
            allCards.Add(card); //Adds the cards to the allCards list
        }

        //Sorting the allCards list by on the card name
        allCards = allCards.OrderBy(card => card.name).ToList();

        //Creates the cards
        createCards();
    }

    //Function to create the cards to be displayed on the menu - AHL(4/12/21)
    private void createCards()
    {
        //Goes through the sorted list and sets the cards to be displayed based on the list
        foreach (Card currCard in allCards)
        {
            GameObject card1 = Instantiate(cardTemplate) as GameObject; //Instantiate a new card
            card1.SetActive(true);

            card1.GetComponentInChildren<CardDisplay>().card = currCard; //Sets the card to a specific sorted card display aspect

            card1.transform.SetParent(cardTemplate.transform.parent, false); //Makes sure the new object is parented correctly to get the effect of the grid based view

            templates.Add(card1); //Adds the new object to the list to be removed later
        }
    }

    //Function to remove all the cards within the allCards list so everything can be refreshed if the player collects or discards cards
    private void refreshList()
    {
        allCards.Clear(); //Clear the list of all cards
        
        //Goes through the list of templates and destroys all the GameObjects
        foreach (GameObject cardTemps in templates)
        {
            Destroy(cardTemps);
        }
    }
}
