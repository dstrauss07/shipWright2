using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{

    [SerializeField] public string ItemName;
    [SerializeField] public int ItemCost = 1;
    public bool itemIsInGameScreen = false;
    [SerializeField] List<Character> attractedCharacters;



    public string GetItemName()
    {
         return ItemName;
    }

    public Character GetAttractedCharacter()
    {
        int diceRoll = Random.Range(0, 100);
        Debug.Log("your rolled " + diceRoll);
        int attractedCharacterIndex = 0;
        if (diceRoll <= 60)
        {
            attractedCharacterIndex = 0;
            return attractedCharacters[attractedCharacterIndex];
        }
        else
        {
            attractedCharacterIndex = Random.Range(1, attractedCharacters.Count);
            return attractedCharacters[attractedCharacterIndex];
        }
 }


}
