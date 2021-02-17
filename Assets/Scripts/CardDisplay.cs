using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    //public Image artworkImage;

    public TMP_Text nameText;
    public TMP_Text elementText;
    public TMP_Text desctiptionText;


    private void Start()
    {
        nameText.text = card.name;
        elementText.text = card.element;
        desctiptionText.text = card.description;
    }
}
