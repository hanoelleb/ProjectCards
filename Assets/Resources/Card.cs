using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Assets/Resources/Cards")]
public class Card : ScriptableObject
{
    [SerializeField]
    int id;

    [SerializeField]
    Sprite cardSprite;

    [SerializeField]
    string group;

    //0 black 1 red
    [SerializeField]
    int color;

    [SerializeField]
    int num; //ace = 0, j = 11, q = 12, k = 13
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite getSprite()
    {
        return cardSprite;
    }

    public int getNum()
    {
        return num;
    }

    public int getColor()
    {
        return color;
    }
}
