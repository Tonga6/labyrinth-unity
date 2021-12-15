using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    List<Card> cards = new List<Card>();
    List<CardHolder> cardHolders = new List<CardHolder>();
    List<int> range = new List<int>();
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
            int rand = Random.Range(0, 24-i);
            cards[i].cardHolder = cardHolders[rand];
            cards[i].transform.SetParent(cardHolders[rand].transform);
            cards[i].transform.localPosition = new Vector3(0, 0, 0);
            cardHolders.RemoveAt(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
