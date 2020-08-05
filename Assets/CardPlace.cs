using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlace : MonoBehaviour, IDropHandler, IDragHandler, IEndDragHandler
{
    bool inValidPlace = false;

    BoxCollider2D dropBox;

    [SerializeField]
    Vector3 start;

    [SerializeField]
    Card card;

    static bool droppedOnSlot;
    static bool noMatch;

    int defaultOrder;

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        print(this.getCard());
        //throw new System.NotImplementedException();
        GameObject droppedObject = eventData.pointerDrag;
        print(droppedObject.GetComponent<CardPlace>().getCard());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inValidPlace)
            print("stopped dragging");
        else
            transform.localPosition = start;

        GetComponent<SpriteRenderer>().sortingOrder = defaultOrder;
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        Vector3 pos;
        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        transform.position = pos;
    }


    // Start is called before the first frame update
    void Start()
    {
        defaultOrder = GetComponent<SpriteRenderer>().sortingOrder;
        start = transform.localPosition;
        dropBox = GetComponent<BoxCollider2D>();

        if (card == null)
            dropBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void setCard(Card newCard)
    {
        card = newCard;
    }

    public Card getCard()
    {
        return this.card;
    }

    public void enableCollider()
    {
        dropBox.enabled = true;
    }
}
