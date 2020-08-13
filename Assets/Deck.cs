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
    int remove;

    bool first = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        remove += 3;
        current++;
        if (current >= cards.Count)
        {
            remove = -3;
            current = -1;
            cardSlots[0].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[0].GetComponent<DeckPlace>().setCard(null);
            cardSlots[0].GetComponent<DeckPlace>().disableCollider();

            cardSlots[1].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[1].GetComponent<DeckPlace>().setCard(null);
            cardSlots[1].GetComponent<DeckPlace>().disableCollider();

            cardSlots[2].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[2].GetComponent<DeckPlace>().setCard(null);
            cardSlots[2].GetComponent<DeckPlace>().disableCollider();
        }
        else
        {
            cardSlots[2].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();
            cardSlots[2].GetComponent<DeckPlace>().enableCollider();
            cardSlots[2].GetComponent<DeckPlace>().setCard(cards[current]);

            cardSlots[1].GetComponent<DeckPlace>().disableCollider();
            cardSlots[0].GetComponent<DeckPlace>().disableCollider();

            if (++current < cards.Count)
            {
                cardSlots[1].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();
                cardSlots[1].GetComponent<DeckPlace>().setCard(cards[current]);
            }
            else
            {
                cardSlots[1].GetComponent<SpriteRenderer>().sprite = null;
                cardSlots[1].GetComponent<DeckPlace>().setCard(null);
            }

            if (++current < cards.Count)
            {
                cardSlots[0].GetComponent<SpriteRenderer>().sprite = cards[current].getSprite();
                cardSlots[0].GetComponent<DeckPlace>().setCard(cards[current]);
            }
            else
            {
                cardSlots[0].GetComponent<SpriteRenderer>().sprite = null;
                cardSlots[0].GetComponent<DeckPlace>().setCard(null);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        current = -1;
        remove = -3;
        first = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCards(List<Card> newCards)
    {
        cards = newCards;
        current = -1;
        remove = -3;

        if (!first)
        {
            cardSlots[0].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[0].GetComponent<DeckPlace>().setCard(null);
            cardSlots[0].GetComponent<DeckPlace>().disableCollider();

            cardSlots[1].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[1].GetComponent<DeckPlace>().setCard(null);
            cardSlots[1].GetComponent<DeckPlace>().disableCollider();

            cardSlots[2].GetComponent<SpriteRenderer>().sprite = null;
            cardSlots[2].GetComponent<DeckPlace>().setCard(null);
            cardSlots[2].GetComponent<DeckPlace>().disableCollider();
        }
    }

    public void handleRemove(Card card, int index)
    {
        cards.RemoveAt(remove);

        cardSlots[index].GetComponent<DeckPlace>().disableCollider();
        cardSlots[index].GetComponent<SpriteRenderer>().sprite = null;
        cardSlots[index].GetComponent<DeckPlace>().setCard(null);

        if (index > 0)
            cardSlots[index-1].GetComponent<DeckPlace>().enableCollider();
    }
}
