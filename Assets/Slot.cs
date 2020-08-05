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

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (cards.Count > 0)
        {
            for (int i = 0; i < cards.Count-1; i++)
            {
                cardPlace[i].GetComponent<SpriteRenderer>().sprite = cardBack;
                cardPlace[i].GetComponent<CardPlace>().setCard(cards[i]);
            }

            cardPlace[cards.Count-1].GetComponent<SpriteRenderer>().sprite = cards[cards.Count-1].getSprite();
            cardPlace[cards.Count - 1].GetComponent<CardPlace>().setCard(cards[cards.Count - 1]);
            cardPlace[cards.Count-1].GetComponent<CardPlace>().enableCollider();
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
}
