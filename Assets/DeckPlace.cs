using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPlace : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int index;
    int defaultOrder;
    Vector3 start;

    BoxCollider2D bc;

    Card card;

    Deck deck;

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData.pointerDrag, 0);
        GameObject obj = eventData.pointerDrag;
        DeckPlace place = obj.GetComponent<DeckPlace>();

        GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    private void SetDraggedPosition(GameObject data, float offSet)
    {
        Vector3 pos;
        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offSet, transform.position.z);
        data.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = start;
        GetComponent<SpriteRenderer>().sortingOrder = defaultOrder;
    }

    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;
        defaultOrder = GetComponent<SpriteRenderer>().sortingOrder;

        bc = GetComponent<BoxCollider2D>();

        deck = GameObject.FindGameObjectWithTag("deck").GetComponent<Deck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card getCard()
    {
        return card;
    }

    public void setCard(Card newCard)
    {
        card = newCard; 
    }

    public void removeCard()
    {
        deck.handleRemove(card, index);
    }

    public void enableCollider()
    {
        bc.enabled = true;
    }

    public void disableCollider()
    {
        bc.enabled = false;
    }


}
