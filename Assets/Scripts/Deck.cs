using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    private List<Card> hand = new List<Card>();

    int handSelectionIndex = 0;
    const int HAND_SIZE_MAX = 3;

    int debugCall = 0;
    int debugMana = 20;

    public bool ActivateSelectedCard(ref int playerMana)
    {
        var selectedCard = hand[handSelectionIndex];
        print("Activating selected card: " + selectedCard.name + " cost: " + selectedCard.manaCost);

        // player has enough mana to cast...
        if (selectedCard.manaCost <= playerMana)
        {
            playerMana = Mathf.Max(0, playerMana - selectedCard.manaCost);
            selectedCard.InstantiateSpell();

            discardPile.Add(selectedCard);
            hand.RemoveAt(handSelectionIndex);

            DrawCard();

            return true;
        }

        return false;
    }

    public void SwapSelectedCard()
    {
        handSelectionIndex++;
        handSelectionIndex %= HAND_SIZE_MAX;

        var selectedCard = hand[handSelectionIndex];

        print("Selected: " + selectedCard.name + " at index " + handSelectionIndex + ". Cost is: " + selectedCard.manaCost);
    }

    // draws a specified amount of cards (default is 1)
    public void DrawCard(int amount = 1)
    {
        string msg = "Drew ";
        int debugCounter = 0;

        while (amount > 0 && hand.Count < HAND_SIZE_MAX)
        {
            if (drawPile.Count == 0) RefillHand();
            hand.Add(drawPile[0]);
            drawPile.RemoveAt(0);
            --amount;
            ++debugCounter;
        }

        msg += debugCounter + " card(s)";
        print(msg);
    }

    // scan discard, hand, and draw lists. if you find a matching card name,
    // destroy that card and return true
    public bool DestroyCard(string name)
    {
        for (int i = discardPile.Count; --i >= 0;)
        {
            if (discardPile[i].name == name)
            {
                discardPile.RemoveAt(i);
                return true;
            }
        }

        for (int i = hand.Count; --i >= 0;)
        {
            if (hand[i].name == name)
            {
                hand.RemoveAt(i);
                DrawCard();
                return true;
            }
        }

        for (int i = drawPile.Count; --i >= 0;)
        {
            if (drawPile[i].name == name)
            {
                drawPile.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    // takes cards from discard and adds it to draw
    void RefillHand()
    {
        print("Moving all discarded cards to the draw pile");

        // shuffle discard list
        ShuffleList(ref discardPile);

        // transfer all cards from discard list to the draw list
        for (int i = discardPile.Count; --i >= 0;)
        {
            drawPile.Add(discardPile[i]);
            discardPile.RemoveAt(i);
        }
    }

    // randomizes the items in a list of cards
    void ShuffleList(ref List<Card> cards)
    {
        for (int i = cards.Count; --i >= 0;)
        {
            int randomIndex = Random.Range(i, cards.Count);

            var tempCard = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = tempCard;
        }
    }

    void DebugPrint()
    {
        debugCall++;
        print(debugCall + ": Discard, hand, draw sizes: " + discardPile.Count + ", " + hand.Count + ", " + drawPile.Count);
    }

    private void Start()
    {
        ShuffleList(ref drawPile);
        DrawCard(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            string msg = "Player mana before: " + debugMana;

            if (ActivateSelectedCard(ref debugMana))
                msg += "... after: " + debugMana;
            else
                msg += "... not enough mana";

            print(msg);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SwapSelectedCard();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DrawCard();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (DestroyCard("Fireball")) print("Destroyed fireball");
            else print("Couldn't find fireball");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            DebugPrint();
        }
    }
}
