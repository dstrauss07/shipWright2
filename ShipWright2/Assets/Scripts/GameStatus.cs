using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] List<Item> gameItems;  //for Debugging
    [SerializeField] List<Character> visitingCharacters; //for Debugging 
    [SerializeField] Item itemToSet;
    [SerializeField] Item setItem1;
    public bool setModeActive = false;



    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameStatus>().Length;

        if (numberGameSessions > 1)
        {
             Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToGameItems(Item item)
    {
        gameItems.Add(item);
    }

    public List<Item> ListGameItems()
    {
        return gameItems;
    }

    public void SetGameItemToSet(Item item)
    {
        itemToSet = item;
    }

    public Item getItemToSet()
    {
        return itemToSet;
    }


    public void SetItem1(Item item)
    {
        setItem1 = item;
    }

    public Item getSetItem1()
    {
        return setItem1;
    }

    public void AddToVisitedCharacters(Character visitedCharacter)
    {
        visitingCharacters.Add(visitedCharacter);
    }

    public List<Character> GetListVisitingCharacters()
    {
        return visitingCharacters;
    }

    public void AddItemToVisitedCharacter(Character visitedCharacter, Item SetGameItem)
    {
        Character visitingCharacterToUpdate = visitingCharacters.Find(c => c.characterName == visitedCharacter.characterName);
        visitingCharacters.Remove(visitingCharacterToUpdate);
        visitingCharacterToUpdate.AddToItemsAttractedBy(SetGameItem);
        visitingCharacterToUpdate.NumberOfTimesVisited++;
        visitingCharacters.Add(visitingCharacterToUpdate);
    }

    public void AddToCharacterVisitsOnly(Character visitedCharacter)
    {
        Character visitingCharacterToUpdate = visitingCharacters.Find(c => c.characterName == visitedCharacter.characterName);
        visitingCharacters.Remove(visitingCharacterToUpdate);
        visitingCharacterToUpdate.NumberOfTimesVisited++;
        visitingCharacters.Add(visitingCharacterToUpdate);
    }



}
