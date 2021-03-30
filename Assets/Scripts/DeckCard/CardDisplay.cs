using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Image artworkImage;
    public TMP_Text nameText;
    public TMP_Text elementText;
    public TMP_Text desctiptionText;

    private void Update()
    {
        if (nameText!=null)
        {
            nameText.text = card.name;
        }

        if (elementText != null)
        {
            elementText.text = card.element;
        }

        if (desctiptionText != null)
        {
            desctiptionText.text = card.description;
        }
    }
}
