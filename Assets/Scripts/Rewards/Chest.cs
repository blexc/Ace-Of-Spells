using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functionality inherits from Reward.cs
public class Chest : Reward
{
    [SerializeField] Card cardReward;

    private void GenerateRandomCard()
    {
        // get all Card scriptable objects and store them in a list
        List<Card> allCardsList = Deck.instance.AllCards;

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
        GenerateRandomCard();
        Deck.instance.AddNewCard(cardReward);
        Destroy(gameObject);
    }
}
