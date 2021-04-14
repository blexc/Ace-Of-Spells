using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class Deck : MonoBehaviour
{
    // singleton
    public static Deck instance;

    // every card in the project should be added to this
    // no duplicates
    // NOT changeable in-game
    public List<Card> AllCards { get { return allCards; } }
    [SerializeField] List<Card> allCards = new List<Card>();

    public List<Card> Hand { get { return hand; } }

    public List<Card> drawPile = new List<Card>();
    [HideInInspector] public List<Card> discardPile = new List<Card>();
    private List<Card> hand = new List<Card>();
    public GameObject player;

    // incremented by quick thinking spell
    public int numTimesToCast = 1; 

    public int handSelectionIndex = 0;
    const int HAND_SIZE_MAX = 3;

    public bool showDebugPrints = false;
    int debugCall = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (instance != null) Destroy(gameObject);
        instance = this;

        ShuffleList(ref drawPile);
        DrawCard(HAND_SIZE_MAX);

        var cm = FindObjectOfType<CardManager>();
        cm.discardUI.text = "" + discardPile.Count; //Updates the discard Num UI
        cm.cardUpdate();
        cm.showSelectedCard(handSelectionIndex);
    }

    // uses the currently selected card (calls selected card's spell function)
    public void ActivateSelectedCard(InputAction.CallbackContext context)
    {
        // do not work unless you've pressed and released left mouse
        if (!context.performed) return;

        var selectedCard = hand[handSelectionIndex];
        var spell = selectedCard.spell;

        if (showDebugPrints)
            print("Activating selected card: " + selectedCard.name);

        if (!spell)
        {
            Debug.LogError("Spell unset for card: " + name);
            return;
        }

        // pass the spellPrefab into PlayerAttack script to spawn it
        var pa = FindObjectOfType<PlayerAttack>();
        if (!pa)
        {
            Debug.LogError("Player Attack script not found in scene.");
            return;
        }

        if (numTimesToCast == 1)
        {
            pa.CastSpell(spell);
        }
        else if (numTimesToCast > 1)
        {
            StartCoroutine(CastSpellRepeated(pa, spell));
        }

        if (player.GetComponent<PlayerStats>().discardCard)
        {
            player.GetComponent<PlayerStats>().discardCard = false;
            discardPile.Add(selectedCard);
            hand.RemoveAt(handSelectionIndex);
            FindObjectOfType<CardManager>().discardUI.text = "" + discardPile.Count; //Updates the discard Num UI
            DrawCard();
        }
       
    }

    /// <summary>
    /// casts the activated spell multiple times every second
    /// reduces numTimesToCast back to 1
    /// </summary>
    /// <param name="pa"></param>
    /// <param name="spell"></param>
    /// <returns></returns>
    IEnumerator CastSpellRepeated(PlayerAttack pa, GameObject spell)
    {
        int i = numTimesToCast;
        while (i > 0)
        {
            pa.CastSpell(spell);
            i--;
            yield return new WaitForSeconds(1f);
        }
        numTimesToCast = 1;
    }

    // changes the currently selected card to the next card in hand
    // draws at the beginning of the draw list
    public void SwapSelectedCard(InputAction.CallbackContext context)
    {
        if(context.performed)
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

    /// <summary>
    /// Returns the number of cards of a specific type in player's hand.
    /// </summary>
    public int NumOfTypeInHand(CardType cardType)
    {
        int num = 0;
        for (int i = hand.Count; --i >= 0;)
        {
            if (hand[i].element == cardType)
                num++;
        }

        return num;
    }

    /// <summary>
    /// Discard from the front of the draw list until you've met the condition
    /// the args describe.
    /// EX: Func(CardType.Fire, false)
    /// Means to draw a card until you've drawn something other than a fire card
    /// </summary>
    /// <returns>The number of cards discarded. Returns -1 is no card exists in the discard and draw pile.</returns>
    public int DiscardCardUntil(CardType cardTypeToRecieve, bool doesWant = true)
    {
        # region check if there exists a card with this condition in the first place
        bool cardExists = false;
        for (int i = 0; !cardExists && i < drawPile.Count; i++)
        {
            if ((drawPile[i].element == cardTypeToRecieve && doesWant) ||
                (drawPile[i].element != cardTypeToRecieve && !doesWant) )
            {
                cardExists = true;
            }
        }
        for (int i = 0; !cardExists && i < discardPile.Count; i++)
        {
            if ((discardPile[i].element == cardTypeToRecieve && doesWant) ||
                (discardPile[i].element != cardTypeToRecieve && !doesWant) )
            {
                cardExists = true;
            }
        }
        if (!cardExists) return -1;
        #endregion

        bool found = false;
        int discardCounter = 0; // num cards discarded

        while (!found)
        {
            // if you've exhausted all cards in the draw pile then 
            if (drawPile.Count == 0) RefillHand();

            if ((drawPile[0].element == cardTypeToRecieve && doesWant) ||
                (drawPile[0].element != cardTypeToRecieve && !doesWant))
            {
                // you've found the card you want, so draw it
                DrawCard();

                found = true;
            }
            else
            {

                // discard the card
                discardPile.Add(drawPile[0]);
                drawPile.RemoveAt(0);

                discardCounter++;
            }
        }

        // check for enemies to see if any have Rot. deal damage to enemies
        // based on the number of cards discarded
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        foreach(EnemyBase e in enemies)
        {
            if (e.HasStatusEffect(StatusEffect.Rot))
            {
                e.TakeDamage(discardCounter);
            }
        }

        return discardCounter;
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
        FindObjectOfType<CardManager>().cardUpdate(); //Updates what is displayed for the cards in hand UI
        FindObjectOfType<CardManager>().deckUI.text = "" + drawPile.Count; //Updates the deck Num UI
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
        FindObjectOfType<CardManager>().cardUpdate(); //Updates what is displayed for the cards in hand UI
        FindObjectOfType<CardManager>().deckUI.text = "" + drawPile.Count; //Updates the deck Num UI
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
}
