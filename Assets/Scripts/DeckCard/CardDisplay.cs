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
        if (nameText!=null && card!=null)
        {
            nameText.text = card.name;
        }

        if (elementText != null && card!=null)
        {
            string cardTypeStr = "";
            switch (card.element)
            {
                case CardType.Fire:
                    cardTypeStr = "Fire";
                    break;
                case CardType.Frost:
                    cardTypeStr = "Frost";
                    break;
                case CardType.Shadow:
                    cardTypeStr = "Shadow";
                    break;
                case CardType.Lightning:
                    cardTypeStr = "Lightning";
                    break;
            }

            elementText.text = cardTypeStr;
        }

        if (desctiptionText != null && card!=null)
        {
            desctiptionText.text = card.description;
        }
    }
}
