using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScreenManager : MonoBehaviour
{
    [SerializeField] public List<Item> allItemsToLoad;
    [SerializeField] public List<Character> allCharactersToLoad;

    GameStatus gameStatus;
    GameStatus.GameData loadedData;
    Item loadedSetItem1;
    public DateTime lastSaveTimeInDateTime;
    public DateTime currentDateTime;
    Character characterToAdd;
    List<Character> visitedCharacters;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        loadedData = gameStatus.LoadGameData();
        visitedCharacters = gameStatus.GetListVisitingCharacters();

        LoadGameItems();
        Item setItem1 = gameStatus.setItem1;

        int NumberOfVisits = CalculateNumberOfVisitsSinceLastSave();
        AddCharactersSinceLastVisit(setItem1, NumberOfVisits);

    }

    

    
    private void LoadGameItems()
    {

        loadedSetItem1 = allItemsToLoad.Find(item => item.ItemName == loadedData.setItem1Name);
        gameStatus.SetItem1(loadedSetItem1);


        foreach (string purchasedItem in loadedData.PurchasedItems)
        {
            Item itemToAdd = allItemsToLoad.Find(item => item.ItemName == purchasedItem);
            gameStatus.AddToGameItems(itemToAdd);
        }

        foreach (GameStatus.visitingCharactersToSerialize visitedCharacter in loadedData.visitingCharacters)
        {
            Character characterToLoad = allCharactersToLoad.Find(character => character.characterName == visitedCharacter.characterName);
            characterToLoad.NumberOfTimesVisited = visitedCharacter.NumberOfTimesVisited;

            foreach (string loadedAttractedItem in visitedCharacter.attractedItems)
            {
                Item attractedItemToLoad = allItemsToLoad.Find(item => item.ItemName == loadedAttractedItem);
                characterToLoad.AddToItemsAttractedBy(attractedItemToLoad);
            }

            gameStatus.AddToVisitedCharacters(characterToLoad);
        }
    }

    private int CalculateNumberOfVisitsSinceLastSave()
    {
        lastSaveTimeInDateTime = Convert.ToDateTime(loadedData.lastSaveTime);
        currentDateTime = DateTime.Now;
        long diffTicks = (currentDateTime - lastSaveTimeInDateTime).Ticks;
        TimeSpan ts = TimeSpan.FromTicks(diffTicks);
        double minutesFromTs = ts.TotalMinutes;
        int MinutesSinceLastSave = Convert.ToInt16(minutesFromTs);
        Debug.Log("Minutes Since Last save" + MinutesSinceLastSave);
        int NumberOfVisits = MinutesSinceLastSave / gameStatus.timeBetweenVisitsWhenAway;
        Debug.Log("Visits Since Last save" + NumberOfVisits);
        return NumberOfVisits;
    }

    private void AddCharactersSinceLastVisit(Item setItem1, int NumberOfVisits)
    {
        for (int visitNum = 0; visitNum < NumberOfVisits; visitNum++)
        {
            characterToAdd = gameStatus.setItem1.GetAttractedCharacter();
            if (!visitedCharacters.Contains(characterToAdd))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setItem1.ItemName);
                characterToAdd.AddToItemsAttractedBy(setItem1);
                characterToAdd.AddToVisits();
                gameStatus.AddToVisitedCharacters(characterToAdd);
                gameStatus.Save();
            }
            else if (visitedCharacters.Contains(characterToAdd))
            {
                Character CharacterToUpdate = visitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
                List<string> ItemListToCheck = CharacterToUpdate.ReturnItemNames();
                if (!ItemListToCheck.Contains(setItem1.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setItem1.ItemName);
                    gameStatus.AddItemToVisitedCharacter(characterToAdd, setItem1);
                    gameStatus.Save();
                }
                else if (ItemListToCheck.Contains(setItem1.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setItem1.ItemName);
                    gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                    gameStatus.Save();
                }
            }
        }
    }
}
