using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishSlot : MonoBehaviour, IDropHandler
{
    Stack<Card> cards;
    // Start is called before the first frame update
    void Start()
    {
        cards = new Stack<Card>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
