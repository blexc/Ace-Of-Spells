using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckList : MonoBehaviour
{
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
    }

    //Function to remove all the cards within the allCards list so everything can be refreshed if the player collects or discards cards
    private void refreshList()
    {
        allCards.Clear();
    }
}
