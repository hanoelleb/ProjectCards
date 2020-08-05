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

    int index;
    int defaultOrder;

    Slot parent;

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        GameObject droppedObject = eventData.pointerDrag;
        CardPlace other = droppedObject.GetComponent<CardPlace>();

        if (this.getCard().getColor() != other.getCard().getColor())
        {
            if (this.getCard().getNum() == other.getCard().getNum() + 1)
            {
                inValidPlace = true;
                Card temp = other.getCard();
                other.getParent().removeCard(other.getIndex());
                parent.setCard(index+1, temp);
            }
        }
       
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

    public void showCard() {
        GetComponent<SpriteRenderer>().sprite = card.getSprite();
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

    public void setDefOrder(int order)
    {
        defaultOrder = order;
    }

    public void setIndex(int newIndex)
    {
        index = newIndex;
    }

    public int getIndex()
    {
        return index;
    }

    public void setParent(Slot slot)
    {
        parent = slot;
    }

    public Slot getParent()
    {
        return parent;
    }
}
