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


    public void CheckGameState()
    {

        bool isWon = false;
        for (int i = 0; i < 3; i++)
        {
            if (!CheckRow(i))
                return;
        }
        Debug.Log("Sequence In Order");
        SequenceComplete();
        return;
    }
    bool CheckRow(int row)
    {
        for (int i = row*8; i < (row+1)*8; i++)
        {
            if (cardHolders[i].id != cardHolders[i].GetComponentInChildren<Card>().correctID)
            {
                return false;
            }

        }
        for (int i = row * 8; i < (row + 1) * 8; i++)
        {
            cardHolders[i].GetComponentInChildren<Card>().LockCard();
        }
        return true;
    }
    void SequenceComplete()
    {
        //gameOver.SetActive(true);
    }
}
