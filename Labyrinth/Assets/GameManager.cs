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

    public TextAsset csvFile; // Reference of CSV file
    //public InputField rollNoInputField;// Reference of rollno input field
    //public InputField nameInputField; // Reference of name input filed
    //public Text contentArea; // Reference of contentArea where records are displayed

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject[] temp = GameObject.FindGameObjectsWithTag("CardHolder");
        for (int i = 0; i < temp.Length; i++)
        {
            cardHolders.Add(temp[i].GetComponent<CardHolder>());
        }

        temp = GameObject.FindGameObjectsWithTag("Card");

        for (int i = 0; i < temp.Length; i++)
        {
            cards.Add(temp[i].GetComponent<Card>());
            int rand = Random.Range(0, 24 - i);
            cards[i].cardHolder = cardHolders[rand];
            cards[i].transform.SetParent(cardHolders[rand].transform);
            cards[i].transform.localPosition = new Vector3(0, 0, 0);
            cardHolders.RemoveAt(rand);
        }
        ReadData();
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
}



