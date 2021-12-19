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
    public GameObject grid;
    public TextAsset csvFile; // Reference of CSV file
    //public InputField rollNoInputField;// Reference of rollno input field
    //public InputField nameInputField; // Reference of name input filed
    //public Text contentArea; // Reference of contentArea where records are displayed

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    public Image[] temps;
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
            cardHolders = new List<CardHolder>(grid.GetComponentsInChildren<CardHolder>());
            
            Debug.Log("cardHolders" + cardHolders.Count);
            List<CardHolder> tempHolders = new List<CardHolder>(cardHolders);
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Card");

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
        }
    }

    void Start()
    {
        //cardData = Resources.Load<CardData>("ScriptObjects/CardData");
        
    }

    public bool CheckGameState()
    {
        foreach (CardHolder ch in cardHolders)
        {
            Debug.Log(ch.id);
        }
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
                Debug.Log("CH id: " + cardHolders[i].id + "Does not align with C id: " + cardHolders[i].GetComponentInChildren<Card>().correctID);
                return false;
            }

        }
        for (int i = row * 8; i < (row + 1) * 8; i++)
        {
            cardHolders[i].GetComponentInChildren<Card>().image.color = Color.yellow;
        }
        Debug.Log("Row " + row + " in Order");
        return true;
    }
}



public enum id
{
    
}
