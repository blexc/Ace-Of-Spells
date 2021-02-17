using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //public Sprite artwork;

    public new string name; 
    public string description;
    public string element;

    public GameObject spell;

    public void InstantiateSpell()
    {
        if (spell == null)
        {
            Debug.LogError("Spell unset for card: " + name);
            return;
        }

        Instantiate(spell);
    }

    public void Print()
    {
        Debug.Log("Card name: " + name + "\nDescription: " + description);
    }
}
