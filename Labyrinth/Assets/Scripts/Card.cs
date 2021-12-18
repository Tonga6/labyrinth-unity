using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
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
        text.text = cardText;
    }
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Begin Drag");
        //image.raycastTarget = false;
        //gr.disabled;
        canvas.overrideSorting = true;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        text.alpha = 1;
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        text.alpha = 0.75f;
        transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);

    }
    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("End Drag Target: " + data.pointerCurrentRaycast.gameObject);
        if (data.pointerCurrentRaycast.gameObject != null)
        {
            Card target = data.pointerCurrentRaycast.gameObject.GetComponent<Card>();
            if (target != null)
            {
                CardHolder temp = target.cardHolder;
                target.MoveTo(cardHolder);
                MoveTo(temp);
                GameManager.gm.CheckGameState();
            }
        }
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void MoveTo(CardHolder target)
    {
        canvas.overrideSorting = true;

        transform.SetParent(target.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        cardHolder = target.GetComponent<CardHolder>();
        canvas.overrideSorting = false;

    }
}
