using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{

    [SerializeField] public string ItemName;
    [SerializeField] public int ItemCost = 1;
    [SerializeField] Character attractedCharacter1;
    public bool characterIsInGameScreen = false;
    //[SerializeField] Characters attractedCharacter2;
    //[SerializeField] Characters attractedCharacter3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName()
    {
         return ItemName;
    }

    public Character GetAttractedCharacter1()
    {
        return attractedCharacter1;
    }


}
