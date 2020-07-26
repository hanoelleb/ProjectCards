using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    int startingNum;

    [SerializeField]
    protected Card mostRecent;

    [SerializeField]
    GameObject[] cardPlace;

    [SerializeField]
    List<Card> cards;

    [SerializeField]
    Sprite cardBack;

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

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
            }

            cardPlace[cards.Count-1].GetComponent<SpriteRenderer>().sprite = cards[cards.Count-1].getSprite();
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
}
