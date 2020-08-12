using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishPlace : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    SpriteRenderer sr;
    List<Card> cards;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cards = new List<Card>();
        print("started.");
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    public void OnDrop(PointerEventData eventData)
    {
        CardPlace other = eventData.pointerDrag.GetComponent<CardPlace>();
        Card newCard = other.getCard();

        print(newCard.name);
        print(newCard.getNum());
        print("count " + cards.Count);
        if (cards.Count == 0 && newCard.getNum() == 1)
        {
            if (other.getChildren().Count == 0)
            {
                other.setValid(true);
                cards.Add(newCard);
                sr.sprite = newCard.getSprite();
                other.getParent().removeCard(other.getIndex());
            }
            return;
        }

        Card recent = cards[cards.Count-1];
        if (recent.GetType() == newCard.GetType() && recent.getNum() == newCard.getNum() - 1)
        {
            //its valid
            if (other.getChildren().Count == 0)
            {
                other.setValid(true);
                cards.Add(newCard);
                sr.sprite = newCard.getSprite();
                other.getParent().removeCard(other.getIndex());
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    //dropped back onto card slot
    public void OnEndDrag(PointerEventData eventData)
    {
        print("here??");
        cards.RemoveAt(cards.Count - 1);
        sr.sprite = cards[cards.Count - 1].getSprite();
    }
}
