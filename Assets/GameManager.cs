using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<Card> deck;
    // Start is called before the first frame update
    void Start()
    {
        shuffle();

        setSlots();

        setDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var r = UnityEngine.Random.Range(i, deck.Count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }

    void setSlots()
    {
        var slots = GameObject.FindGameObjectsWithTag("slot");

        for (int i = 0; i < slots.Length; i++)
        {
            List<Card> newCards = new List<Card>();
            var startNum = slots[i].GetComponent<Slot>().getStartingNum();
            for (int j = 0; j < startNum; j++)
            {
                newCards.Add(deck[j]);
                deck.RemoveAt(j);
            }
            slots[i].GetComponent<Slot>().setCards(newCards);
        }
    }

    void setDeck()
    {
        var cardDeck = GameObject.FindGameObjectWithTag("deck");
        cardDeck.GetComponent<Deck>().setCards(deck);
    }
}
