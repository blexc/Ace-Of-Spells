using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// functionality inherits from Reward.cs
public class Chest : Reward
{
    [SerializeField] Card cardReward;
    [SerializeField] GameObject RewardMenu; //Menu Popup for the reward to provide all the information needed
    [SerializeField] GameObject cardDisplay; //The card game object being displayed on the menu

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

        //Adjusts aspects on the Reward Menu
        cardDisplay.GetComponent<CardDisplay>().card = cardReward;

        //Sets up the menu system
        Time.timeScale = 0f;
        RewardMenu.SetActive(true);
        FindObjectOfType<PlayerInput>().SwitchCurrentActionMap("Menu");
    }

    /// <summary>
    /// Accepts the card reward and places it into the Deck and keeps the game going - AHL (4/29/21)
    /// </summary>
    public void AcceptReward()
    {
        //If the player accepts the reward then it will be added to their deck of spells
        Deck.instance.AddNewCard(cardReward);

        //Updates the card UI to show the new card added to the deck
        FindObjectOfType<CardManager>().cardUpdate();

        //Sets the card scriptable object to isObtained as the card is now in the players deck
        cardReward.isObtained = true;

        //The player will then resumes the game
        Time.timeScale = 1f;
        RewardMenu.SetActive(false);
        FindObjectOfType<PlayerInput>().SwitchCurrentActionMap("Gameplay");

        //Then it finally destroys the Chest Game Object
        Destroy(gameObject);
    }

    /// <summary>
    /// Declines the Card Reward and moves on with the game - AHL (4/29/21)
    /// </summary>
    public void DeclineReward()
    {
        //The player will then resumes the game without adding the card to the deck
        Time.timeScale = 1f;
        RewardMenu.SetActive(false);
        FindObjectOfType<PlayerInput>().SwitchCurrentActionMap("Gameplay");

        //Then it finally destroys the Chest Game Object
        Destroy(gameObject);
    }
}
