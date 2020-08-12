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

    int dragging = 1;

    List<GameObject> children;

    public void OnDrag(PointerEventData eventData)
    {
        dragging = 0;
        children = new List<GameObject>();
        int i = index;
        int j = 0;
        while (parent.getCard(i) != null && i < parent.getCardLen()-1)
        {
            if ((i + 1) < transform.parent.childCount)
            {
                GameObject child = transform.parent.GetChild(i + 1).gameObject;
                CardPlace childPlace = child.GetComponent<CardPlace>();
                j++;
                childPlace.disableCollider();
                if (childPlace.getCard() != null)
                    children.Add(child);
                SetDraggedPosition(child, (-0.5f*j));
                dragging++;
                i++;
            }
            else
                break;
        }

        SetDraggedPosition(eventData.pointerDrag, 0);
        GameObject obj = eventData.pointerDrag;
        CardPlace place = obj.GetComponent<CardPlace>();

        GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    public int getDragging()
    {
        //how many cards are being dropped
        return dragging;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        //handle card from deck
        DeckPlace fromDeck = droppedObject.GetComponent<DeckPlace>();
        if (fromDeck != null)
        {
            Card temp = fromDeck.getCard();
            if (this.getCard() == null && index == 0 && temp.getNum() == 13)
            {
                parent.setCard(index+1, temp);
            }


            //other cases
            if (this.getCard().getColor() != fromDeck.getCard().getColor())
            {
                if (this.getCard().getNum() == fromDeck.getCard().getNum() + 1)
                {
                    parent.setCard(index+1, temp);
                    fromDeck.removeCard();
                }
            }

            return;
        }

        CardPlace other = droppedObject.GetComponent<CardPlace>();


        //Handle king on empty slot
        if (this.getCard() == null && index == 0 && other.getCard().getNum() == 13) {
            other.inValidPlace = true;
            Card temp = other.getCard();

            int amount = other.getDragging();
            parent.setCard(index, temp);
            for (var i = 0; i < other.children.Count; i++)
            {
                temp = other.children[i].GetComponent<CardPlace>().getCard();
                parent.setCard(index + i + 1, temp);
            }

            for (var i = 0; i < eventData.pointerDrag.GetComponent<CardPlace>().dragging; i++)
                other.getParent().removeCard(other.getIndex() + i + 1);
            other.getParent().removeCard(other.getIndex());
            return;
        }

        //Handle all other cases
        if ( this.getCard().getColor() != other.getCard().getColor() )
        {
            if (this.getCard().getNum() == other.getCard().getNum() + 1)
            {
                other.inValidPlace = true;
                Card temp;

                if (other.children.Count > 0)
                {
                    for (var i = 0; i < other.children.Count; i++)
                    {
                        temp = other.children[i].GetComponent<CardPlace>().getCard();
                        if (temp != null)
                            parent.setCard(this.index + i + 2, temp);
                    }
                }
                temp = other.getCard();
                parent.setCard(this.index + 1, temp);

                int firstToRemove = other.getIndex();
                int amount = eventData.pointerDrag.GetComponent<CardPlace>().dragging;
                if (amount > 0)
                {
                    other.getParent().removeCard(firstToRemove);
                    for (var i = 0; i < amount; i++)
                        other.getParent().removeCard(other.getIndex() + i + 1);
                    //other.getParent().removeCard(firstToRemove);
                    other.children.Clear();
                }
                else
                    other.getParent().removeCard(other.getIndex());
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = start;

        inValidPlace = false;
        dragging = 0;

        int i = index;

        while (parent.getCard(i) != null && i < parent.getCardLen()-1)
        {
            if ((i + 1) < transform.parent.childCount)
            {
                GameObject child = transform.parent.GetChild(i + 1).gameObject;
                child.transform.localPosition = child.GetComponent<CardPlace>().start;
                child.GetComponent<CardPlace>().enableCollider();
                dragging++;
                i++;
            }
            else
                break;
        }

        GetComponent<SpriteRenderer>().sortingOrder = defaultOrder;
    }

    private void SetDraggedPosition(GameObject data, float offSet)
    {
        Vector3 pos;
        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offSet, transform.position.z);
        data.transform.position = pos;
    }


    // Start is called before the first frame update
    void Start()
    {
        defaultOrder = GetComponent<SpriteRenderer>().sortingOrder;
        start = transform.localPosition;
        dropBox = GetComponent<BoxCollider2D>();

        if (card == null)
            dropBox.enabled = false;

        children = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite == null && index != 0)
            disableCollider();
    }

    public void showCard() {
        if (card != null)
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

    public void disableCollider()
    {
        dropBox.enabled = false;
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

    public List<GameObject> getChildren()
    {
        return children;
    }

    public void setValid(bool val)
    {
        inValidPlace = val;
    }
}
