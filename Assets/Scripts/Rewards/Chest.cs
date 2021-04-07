using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functionality inherits from Reward.cs
public class Chest : Reward
{
    Card cardReward;

    private void Awake()
    {
        // get all Card scriptable objects and store them in a list
        Card[] allCardsArr = Resources.FindObjectsOfTypeAll<Card>();
        List<Card> allCardsList = new List<Card>(allCardsArr);

        // choose a random one and set it be Chest's reward 
        // keep picking a card until its not NA (to prevent locked card)
        int i;
        do
        {
            i = Random.Range(0, allCardsList.Count);
        }
        while (allCardsList[i].element == CardType.NA);

        cardReward = allCardsList[i];
    }

    public override void RecieveReward()
    {
        Deck.instance.AddNewCard(cardReward);
        Destroy(gameObject);
    }
}
