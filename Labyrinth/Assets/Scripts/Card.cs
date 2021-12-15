using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image image;
    GraphicRaycaster gr;
    Canvas canvas;
    
    public CardHolder cardHolder
    { get; set; }
    public int correctID
    { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        image = GetComponent<Image>();
        cardHolder = GetComponentInParent<CardHolder>();
        gr = GetComponent<GraphicRaycaster>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Begin Drag");
        //image.raycastTarget = false;
        //gr.disabled;
        canvas.overrideSorting = true;
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
                target.MoveTo(cardHolder.gameObject);
                transform.SetParent(temp.transform);
                transform.localPosition = new Vector3(0, 0, 0);
                cardHolder = temp;
            }
            //image.raycastTarget = true;
        }
        //Debug.Log("Drag End: No Target");
        canvas.overrideSorting = false;

    }

    public void MoveTo(GameObject target)
    {
        canvas.overrideSorting = true;

        transform.SetParent(target.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        cardHolder = target.GetComponent<CardHolder>();
        canvas.overrideSorting = false;

    }
}
