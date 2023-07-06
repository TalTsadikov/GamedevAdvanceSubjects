using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR;

public class CardManager : MonoBehaviour
{
    [SerializeField] Deck playerDeck;
    [SerializeField] GameObject playerHandAligner;
    [SerializeField] List<GameObject> playerHand;


    [SerializeField] GameObject cardPrefab;

    private CardSaveData playerHandData;

    const string SAVE_FILE_PATH = "/handsavefile.csf";


    void Start()
    {
        DealStartingHand();
    }

    public void DealStartingHand()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject card = Instantiate(cardPrefab, playerHandAligner.transform);
            playerHand.Add(card);
            CardDisplay cardInformation = card.GetComponent<CardDisplay>();

            cardInformation.card = playerDeck.deckCards[Random.Range(0,9)];
        }
    }

    public void SaveHandBinary()
    {
        playerHandData = new CardSaveData(playerHand.Count);

        for (int i = 0; i < playerHand.Count; i++)
        {
            CardDisplay cardInformation = playerHand[i].GetComponent<CardDisplay>();
            playerHandData.nameList[i] = cardInformation.card.cardName;
            playerHandData.descList[i] = cardInformation.card.cardDescription;

            playerHandData.costList[i] = cardInformation.card.cost;
            playerHandData.powerList[i] = cardInformation.card.power;
        }

        string path = Application.persistentDataPath + SAVE_FILE_PATH;

        FileStream stream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, playerHandData);
        stream.Close();
    }

    public void LoadHandBinary()
    {
        string path = Application.persistentDataPath + SAVE_FILE_PATH;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        object deserialized = formatter.Deserialize(stream);
        playerHandData = (CardSaveData)deserialized;

        if (playerHand.Count >= 0)
        {
            foreach (GameObject oldCard in playerHand)
            {
                Destroy(oldCard);
            }

            playerHand.Clear();
        }

        for (int i = 0; i < playerHandData.nameList.Length; i++)
        {
            GameObject card = Instantiate(cardPrefab, playerHandAligner.transform);
            playerHand.Add(card);

            CardDisplay cardInformation = card.GetComponent<CardDisplay>();

            var savedCard = new Card();
            savedCard.cardName = playerHandData.nameList[i];
            savedCard.cardDescription = playerHandData.descList[i];
            savedCard.cost = playerHandData.costList[i];
            savedCard.power = playerHandData.powerList[i];

            Sprite sp = Resources.Load<Sprite>($"Sprites/{savedCard.cardName}");
            savedCard.cardSprite = sp;

            cardInformation.card = savedCard;
        }
    }
}
