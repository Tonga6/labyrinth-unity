using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public bool locked = false;
    GraphicRaycaster gr;
    Canvas canvas;
    
    public CardHolder cardHolder
    { get; set; }
    public int correctID;
    public string cardText;
    TextMeshProUGUI text;
    // Start is called before the first frame update

    void Awake()
    {
        cardText = "";
        canvas = GetComponent<Canvas>();
        canvas.sortingLayerID = 0;
        image = GetComponent<Image>();
        cardHolder = GetComponentInParent<CardHolder>();
        gr = GetComponent<GraphicRaycaster>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.alpha = 0.75f;
    }
    public void LoadData(string[] fields) {
        correctID = int.Parse(fields[0]);
        text.text = fields[1];
    }
    public void DisplayData()
    {
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = cardText;
    }
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Begin Drag");
        canvas.overrideSorting = true;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        text.alpha = 1;
        transform.DOScale(1.1f,0.25f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        text.alpha = 0.75f;
        transform.DOScale(1, 0.25f);
    }
    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (data.pointerCurrentRaycast.gameObject != null)
        {
            Card target = data.pointerCurrentRaycast.gameObject.GetComponent<Card>();
            if (target != null)
            {
                CardHolder temp = target.cardHolder;
                target.MoveTo(cardHolder);
                MoveTo(temp);
                canvas.overrideSorting = false;

                GameManager.gm.CheckGameState();
            }
        }
        
    }

    public void MoveTo(CardHolder target)
    {
        transform.SetParent(target.transform);
        cardHolder = target.GetComponent<CardHolder>();

        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
        //transform.DOScale(1.5f, 0.25f);
        transform.DOMove(target.transform.position, 1).OnComplete
               (
                  () =>
                  {
                      Debug.Log("DOMove complete");
                      transform.DOScale(1, 0.25f);                      
                      canvas.sortingOrder = 0;
                      canvas.overrideSorting = false;
                  }
               );
    }


    public void LockCard()
    {
        image.DOFade(0.6f, 0.5f);
        image.raycastTarget = false;
        this.enabled = false;
    }
}
