using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functionality inherits from Reward.cs
public class Chest : Reward
{
    Card cardReward;
    public Card[]  cards = new Card[18];

    private void Awake()
    {
        // get all Card scriptable objects and store them in a list
        Card[] allCardsArr = Resources.FindObjectsOfTypeAll<Card>();
        List<Card> allCardsList = new List<Card>(allCardsArr);
        Debug.Log(allCardsList.Count);
        Debug.Log(allCardsArr.Length);

        // choose a random one and set it be Chest's reward 
        // keep picking a card until its not NA (to prevent locked card)
        int i;
        do
        {
            i = Random.Range(0, cards.Length);
        }
        while (cards[i].element == CardType.NA);

        cardReward = cards[i];
    }

    public override void RecieveReward()
    {
        Deck.instance.AddNewCard(cardReward);
        Destroy(gameObject);
    }
}
