﻿using System.Collections;
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
    [SerializeField] private Image favoriteStar = null;

    private void Update()
    {

        if (artworkImage != null && card != null)
        {
            artworkImage.sprite = card.image;
        }

        if (nameText != null && card != null)
        {
            nameText.text = card.name;
        }

        if (elementText != null && card != null)
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

        if (desctiptionText != null && card != null)
        {
            desctiptionText.text = card.description;
        }

        //Sets the Background card color
        if (card != null)
        {
            gameObject.GetComponent<Image>().color = card.backgroundColor;

            if (card.isFavorite && card.isObtained)
            {
                favoriteStar.gameObject.SetActive(true);
            }
            else if (favoriteStar.isActiveAndEnabled && !card.isFavorite)
            {
                favoriteStar.gameObject.SetActive(false);
            }
        }
    }
}
