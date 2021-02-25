using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Deck : MonoBehaviour
{
    public List<Card> Hand { get { return hand; } }

    public List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    private List<Card> hand = new List<Card>();

    public int handSelectionIndex = 0;
    const int HAND_SIZE_MAX = 3;

    public bool showDebugPrints = false;
    int debugCall = 0;

    // uses the currently selected card (calls selected card's spell function)
    public void ActivateSelectedCard()
    {
        var selectedCard = hand[handSelectionIndex];
        if (showDebugPrints)
            print("Activating selected card: " + selectedCard.name);

        selectedCard.InstantiateSpell();

        discardPile.Add(selectedCard);
        hand.RemoveAt(handSelectionIndex);

        FindObjectOfType<CardManager>().discardUI.text = "" + discardPile.Count; //Updates the discard Num UI

        DrawCard();
    }

    // changes the currently selected card to the next card in hand
    public void SwapSelectedCard()
    {
        handSelectionIndex++;
        handSelectionIndex %= hand.Count;

        FindObjectOfType<CardManager>().showSelectedCard(handSelectionIndex);

        if (showDebugPrints)
        {
            var selectedCard = hand[handSelectionIndex];
            print("Selected: " + selectedCard.name + " at index " + handSelectionIndex);
        }
    }

    // draws a specified amount of cards (default is 1)
    public void DrawCard(int amount = 1)
    {
        int cardsDrew = 0;
        while (amount > 0 && hand.Count < HAND_SIZE_MAX)
        {
            if (drawPile.Count == 0) RefillHand();
            hand.Add(drawPile[0]);
            drawPile.RemoveAt(0);
            --amount;
            cardsDrew++;
        }

        FindObjectOfType<CardManager>().cardUpdate(); //Updates what is displayed for the cards in hand UI
        FindObjectOfType<CardManager>().deckUI.text = "" + drawPile.Count; //Updates the deck Num UI
    }

    // adds a new card to the draw pile
    // used for when player finds a card in-game
    public void AddNewCard(Card card)
    {
        drawPile.Add(card);
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

        FindObjectOfType<CardManager>().discardUI.text = "" + discardPile.Count; //Updates the discard Num UI
        return false;
    }

    // takes cards from discard and adds it to draw
    void RefillHand()
    {
        if (showDebugPrints)
            print("Moving all discarded cards to the draw pile");

        // shuffle discard list
        ShuffleList(ref discardPile);

        // transfer all cards from discard list to the draw list
        for (int i = discardPile.Count; --i >= 0;)
        {
            drawPile.Add(discardPile[i]);
            discardPile.RemoveAt(i);
        }

        FindObjectOfType<CardManager>().discardUI.text = "" + discardPile.Count; //Updates the discard Num UI
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
        if (showDebugPrints)
        {
            debugCall++;
            print(debugCall + ": Discard, hand, draw sizes: " + discardPile.Count + ", " + hand.Count + ", " + drawPile.Count);
        }
    }

    private void Start()
    {
        ShuffleList(ref drawPile);
        DrawCard(HAND_SIZE_MAX);
        FindObjectOfType<CardManager>().discardUI.text = "" + discardPile.Count; //Updates the discard Num UI
        FindObjectOfType<CardManager>().cardUpdate();
        FindObjectOfType<CardManager>().showSelectedCard(handSelectionIndex);
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateSelectedCard();
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
            bool didDestroy = DestroyCard("Fireball");

            if (showDebugPrints)
            {
                if (didDestroy) print("Destroyed a Fireball");
                else print("Did not find Fireball object to destroy");
            }
        }
    }
    */

    public void SwitchActiveCard(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Right Click!");
            SwapSelectedCard();
        }
    }
}
