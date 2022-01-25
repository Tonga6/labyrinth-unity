using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    List<GameObject> tempCards;
    List<CardHolder> tempHolders;
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

            tempHolders = new List<CardHolder>(cardHolders);
            tempCards = GameObject.FindGameObjectsWithTag("Card").ToList();

            SetupBoard();
            
        }
        
    }
    void SetupBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i % 8 == 0)
            {
                AssignCardToHolder(i, 0);
                cards[cards.Count - 1].LockCard();
            }
            else
            {
                int rand = Random.Range(0, 8 - i % 8);
                AssignCardToHolder(i, rand);
            }
        }
    }
    void AssignCardToHolder(int cardIndex, int holderIndex)
    {
        Card temp = tempCards[cardIndex].GetComponent<Card>();
        cards.Add(temp);
        temp.cardHolder = tempHolders[holderIndex];
        temp.transform.SetParent(tempHolders[holderIndex].transform);
        temp.transform.localPosition = new Vector3(0, 0, 0);
        temp.correctID = cardData.ids[cardIndex];
        temp.cardText = cardData.cardText[cardIndex];
        temp.DisplayData();
        tempHolders.RemoveAt(holderIndex);
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
