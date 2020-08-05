using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour { 
    [SerializeField]
    int startingNum;

    [SerializeField]
    protected Card mostRecent;

    [SerializeField]
    GameObject[] cardPlace;

    [SerializeField]
    List<Card> cards;

    [SerializeField]
    private Sprite cardBack;

    bool first = true;
    
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            if (cards.Count > 0)
            {
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    cardPlace[i].GetComponent<SpriteRenderer>().sprite = cardBack;
                    cardPlace[i].GetComponent<CardPlace>().setCard(cards[i]);
                    cardPlace[i].GetComponent<CardPlace>().setIndex(i);
                    cardPlace[i].GetComponent<CardPlace>().setParent(this);
                }

                cardPlace[cards.Count - 1].GetComponent<SpriteRenderer>().sprite = cards[cards.Count - 1].getSprite();
                cardPlace[cards.Count - 1].GetComponent<CardPlace>().setCard(cards[cards.Count - 1]);
                cardPlace[cards.Count - 1].GetComponent<CardPlace>().enableCollider();
                cardPlace[cards.Count - 1].GetComponent<CardPlace>().setIndex(cards.Count - 1);
                cardPlace[cards.Count - 1].GetComponent<CardPlace>().setParent(this);
            }
            first = false;
        }
    }

    public int getStartingNum()
    {
        return startingNum;
    }

    public void setCards(List<Card> newCards)
    {
        cards = newCards;
    }

    public Card getCard(int i)
    {
        return cards[i];
    }

    public void setCard(int cardIndex, Card card)
    {
        cards.Add(card);
        //cards[cardIndex] = card;
        cardPlace[cardIndex].GetComponent<SpriteRenderer>().sprite = card.getSprite();
        cardPlace[cardIndex].GetComponent<CardPlace>().setIndex(cardIndex);
        cardPlace[cardIndex].GetComponent<CardPlace>().enableCollider();
        cardPlace[cardIndex].GetComponent<CardPlace>().setCard(card);
        cardPlace[cardIndex].GetComponent<CardPlace>().setParent(this);
    }

    public void removeCard(int cardIndex)
    {

        cardPlace[cardIndex].GetComponent<CardPlace>().setCard(null);
        cardPlace[cardIndex].GetComponent<SpriteRenderer>().sprite = null;
        cardIndex--;
        if (cardIndex >= 0)
        {
            cardPlace[cardIndex].GetComponent<CardPlace>().showCard();
            cardPlace[cardIndex].GetComponent<CardPlace>().enableCollider();
        }
    }
}
