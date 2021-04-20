using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //public Sprite artwork;

    public new string name; 
    public string description;
    public CardType element;

    public GameObject spell;

    public bool isObtained; //Variable to keep track of if the card has been collected to show in the card collection menu
    
    [HideInInspector]
    public bool isFavorite; //Variable to keep track of if the card has been favorited or not

    public void Print()
    {
        Debug.Log("Card name: " + name + "\nDescription: " + description);
    }
}
