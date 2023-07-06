using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public Sprite cardSprite;

    public int cost;
    public int power;
}
