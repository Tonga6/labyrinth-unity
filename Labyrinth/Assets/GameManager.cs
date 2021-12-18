using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    List<Card> cards = new List<Card>();
    List<CardHolder> cardHolders = new List<CardHolder>();
    List<int> range = new List<int>();
    public CardData cardData;
    //public ScriptableObject cardData;
    public TextAsset csvFile; // Reference of CSV file
    //public InputField rollNoInputField;// Reference of rollno input field
    //public InputField nameInputField; // Reference of name input filed
    //public Text contentArea; // Reference of contentArea where records are displayed

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter


    public static GameManager gm;

    private void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
        }
    }

    void Start()
    {
        //cardData = Resources.Load<CardData>("ScriptObjects/CardData");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("CardHolder");
        for (int i = 0; i < temp.Length; i++)
        {
            cardHolders.Add(temp[i].GetComponent<CardHolder>());
        }
        List<CardHolder> tempHolders = new List<CardHolder>(cardHolders);
        temp = GameObject.FindGameObjectsWithTag("Card");

        for (int i = 0; i < temp.Length; i++)
        {
            cards.Add(temp[i].GetComponent<Card>());
            int rand = Random.Range(0, 24 - i);
            cards[i].cardHolder = tempHolders[rand];
            cards[i].transform.SetParent(tempHolders[rand].transform);
            cards[i].transform.localPosition = new Vector3(0, 0, 0);
            cards[i].correctID = cardData.ids[i];
            cards[i].cardText = cardData.cardText[i];
            cards[i].DisplayData();
            tempHolders.RemoveAt(rand);

        }
        //ReadData();
    }
    // Read data from CSV file
    private void ReadData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
        Debug.Log(records.Length);
        Debug.Log(cards.Count);
        for(int i = 1; i < records.Length; i++)
        {
            string[] fields = records[i].Split(fieldSeperator);
            cards[i - 1].LoadData(fields);
        }
    }
    public bool CheckGameState()
    {
        Debug.Log(cardHolders.Count);
        bool isWon = false;
        for (int i = 0; i < 3; i++)
        {
            if (!CheckRow(i))
                return false;
        }
        Debug.Log("Sequence In Order");
        return true;
    }
    bool CheckRow(int row)
    {
        for (int i = row*8; i < (row+1)*8; i++)
        {
            if (cardHolders[i].id != cardHolders[i].GetComponentInChildren<Card>().correctID)
            {
                //Debug.Log("Sequence Still Incorrect");
                return false;
            }

        }
        Debug.Log("Row " + row + " in Order");
        return true;
    }
}



public enum id
{
    
}
