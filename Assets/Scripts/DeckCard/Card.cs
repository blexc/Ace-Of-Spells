using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //public Sprite artwork;

    public Sprite image;
    public new string name; 
    public string description;
    public CardType element;
    public Color backgroundColor = Color.white; // default (in the advent of github probs)

    public GameObject spell;

    public bool isObtained; //Variable to keep track of if the card has been collected to show in the card collection menu
    
    public bool isFavorite; //Variable to keep track of if the card has been favorited or not

    public void Print()
    {
        Debug.Log("Card name: " + name + "\nDescription: " + description);
    }
}
