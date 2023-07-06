using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSaveData
{
    public string[] nameList;
    public string[] descList;

    public int[] costList;
    public int[] powerList;

    public CardSaveData(int size)
    {
        nameList = new string[size];
        descList = new string[size];
        costList = new int[size];
        powerList = new int[size];
    }
}
