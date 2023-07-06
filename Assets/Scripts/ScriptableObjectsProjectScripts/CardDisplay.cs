using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;

    [SerializeField] Image cardImage;

    [SerializeField] TextMeshProUGUI cardCost;
    [SerializeField] TextMeshProUGUI cardPower;


    void Start()
    {
        cardName.text = card.cardName;
        cardDescription.text = card.cardDescription;

        cardImage.sprite = card.cardSprite;

        cardCost.text = $"{card.cost}";
        cardPower.text = $"{card.power}";
    }
}
