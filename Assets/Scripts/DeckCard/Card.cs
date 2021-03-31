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

    public void Print()
    {
        Debug.Log("Card name: " + name + "\nDescription: " + description);
    }
}
