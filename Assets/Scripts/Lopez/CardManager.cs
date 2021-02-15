using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    //Variable Initalization/Declaration
    public GameObject card1, card2, card3; //Card variables to get the card templates from the deck.

    //**AHL - Temporary Variables to hold the text**
    private Text nameText;
    private Text elementText;
    private Text desctiptionText;

    /// <summary>
    /// AHL (2/15/21) - Function that takes in the card that was used and then updates it based off the deck pull
    /// </summary>
    private void cardUpdate(Card card)
    {
        this.GetComponent<Text>().text = card.name;
       //     Find("Name").GetComponent<Text>() = card.name;
        elementText.text = card.element;
        desctiptionText.text = card.description;
    }
    
    // Start is called before the first frame update
    void Start()
    {
          
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
