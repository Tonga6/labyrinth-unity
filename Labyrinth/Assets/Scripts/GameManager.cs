using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
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
            gameOver.SetActive(false);
            cardHolders = new List<CardHolder>(grid.GetComponentsInChildren<CardHolder>());

            List<CardHolder> tempHolders = new List<CardHolder>(cardHolders);
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Card");

            cards.Add(temp[0].GetComponent<Card>());
            cards[0].cardHolder = tempHolders[0];
            cards[0].transform.SetParent(tempHolders[0].transform);
            cards[0].transform.localPosition = new Vector3(0, 0, 0);
            cards[0].correctID = cardData.ids[0];
            cards[0].cardText = cardData.cardText[0];
            cards[0].DisplayData();
            tempHolders.RemoveAt(0);

            for (int i = 1; i < temp.Length; i++)
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
        cards[0].LockCard();
    }


    public void CheckGameState()
    {
        bool isWon = false;
        for (int i = 0; i < 3; i++)
        {
            if (!CheckRows(i))
                return;
        }
        Debug.Log("Sequence In Order");
        SequenceComplete();
        return;
    }
    bool CheckRows(int row)
    {
        for (int i = row*8; i < (row+1)*8; i++)
        {
            if (cardHolders[i].id != cardHolders[i].GetComponentInChildren<Card>().correctID)
            {
                return false;
            }

        }
        LockRow(row);
        return true;
    }
    void LockRow(int row)
    {
        for (int i = row * 8; i < (row + 1) * 8; i++)
        {
            cardHolders[i].GetComponentInChildren<Card>().LockCard();
        }
    }
    void SequenceComplete()
    {
        //gameOver.SetActive(true);
    }
}
