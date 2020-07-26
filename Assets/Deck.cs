using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    List<Card> cards;

    [SerializeField]
    GameObject[] cardSlots;

    int current;

    public void OnPointerClick(PointerEventData eventData)
    {
        current++;
        if (current >= cards.Count)
        {
            current = -1;
            cardSlots[0].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[1].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[2].GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            cardSlots[2].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();

            if (++current < cards.Count)
                cardSlots[1].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();
            else
                cardSlots[1].GetComponent<SpriteRenderer>().sprite = null;

            if (++current < cards.Count)
                cardSlots[0].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();
            else
                cardSlots[0].GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        current = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
