using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckList : MonoBehaviour
{
    [SerializeField]
    private GameObject cardTemplate; //Variable to hold and create the cards for the deckList screen

    public List<Card> allCards; //A list of all the cards currently in the Discard, Hand, and Draw piles
    public Deck deck; //The deck script to keep track of the lists for the different card piles 

    //Function is called when the UI becomes active - AHL (4/12/21)
    private void OnEnable()
    {
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
        //Goes through the sorted list and sets the careds to be displayed based on the list
        foreach (Card currCard in allCards)
        {
            //If the template is null then we set the first card to be this one
            if (cardTemplate.GetComponentInChildren<CardDisplay>().card != null)
            {
                cardTemplate.GetComponentInChildren<CardDisplay>().card = currCard;
            }

            //If the template isn't null then we create cards for the rest of the sorted list to be displayed
            else
            {
                GameObject card1 = Instantiate(cardTemplate) as GameObject;
                card1.SetActive(true);

                card1.GetComponentInChildren<CardDisplay>().card = currCard;

                card1.transform.SetParent(cardTemplate.transform.parent, false);
            }
        }
    }

    //Function to remove all the cards within the allCards list so everything can be refreshed if the player collects or discards cards
    private void refreshList()
    {
        allCards.Clear();

        /*
        GameObject go = GameObject.Find("Card Template(Clone)");

        //Remove all the UI Card Templates
        do
        {
            Destroy(go);
            go = GameObject.Find("Card Template(Clone)");
        } while (go != null);*/
    }
}
