using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScreenManager : MonoBehaviour
{
    [SerializeField] public List<Item> allItemsToLoad;
    [SerializeField] public List<Character> allCharactersToLoad;
    [SerializeField] public List<Character> recentVisitedCharacters;


    GameStatus gameStatus;
    GameObject recentVisitorPanel;
    GameStatus.GameData loadedData;
    Item loadedSetItem1;
    Item loadedSetItem2;
    Item loadedSetItem3;
    public DateTime lastSaveTimeInDateTime;
    public DateTime currentDateTime;
    Character characterToAdd;
    List<Character> visitedCharacters;

    // Start is called before the first frame update
    void Awake()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        loadedData = gameStatus.LoadGameData();
        visitedCharacters = gameStatus.GetListVisitingCharacters();
        LoadGameItems();
        Item setItem1 = gameStatus.setItem1;
        Item setItem2 = gameStatus.setItem2;
        Item setItem3 = gameStatus.setItem3;
        
        recentVisitedCharacters = new List<Character>();
        int NumberOfVisits = CalculateNumberOfVisitsSinceLastSave();
        if (setItem1 != null)
        { AddCharactersSinceLastVisit1(setItem1, NumberOfVisits); }
        if (setItem2 != null)
        { AddCharactersSinceLastVisit2(setItem2, NumberOfVisits); }
        if (setItem3 != null)
        { AddCharactersSinceLastVisit3(setItem3, NumberOfVisits); }
        gameStatus.Save();
    }


    private void Start()
    {
        if (recentVisitedCharacters.Count == 0 || recentVisitedCharacters == null)
        {
           GameObject.Find("VisitedPanel").SetActive(false);
            GameObject.Find("VisitedPanel2").SetActive(true);
        }
        else
        {
            GameObject.Find("VisitedPanel").SetActive(true);
            GameObject.Find("VisitedPanel2").SetActive(false);
        }

    }



    private void LoadGameItems()
    {
        loadedSetItem1 = allItemsToLoad.Find(item => item.ItemName == loadedData.setItem1Name);
        loadedSetItem2 = allItemsToLoad.Find(item => item.ItemName == loadedData.setItem2Name);
        loadedSetItem3 = allItemsToLoad.Find(item => item.ItemName == loadedData.setItem3Name);
        gameStatus.SetItem1(loadedSetItem1);
        gameStatus.SetItem2(loadedSetItem2);
        gameStatus.SetItem3(loadedSetItem3);
        foreach (string purchasedItem in loadedData.PurchasedItems)
        {
            Item itemToAdd = allItemsToLoad.Find(item => item.ItemName == purchasedItem);
            gameStatus.AddToGameItems(itemToAdd);
        }

        foreach (string pictureTakenName in loadedData.picturesTaken)
        {
            gameStatus.AddAPicture(pictureTakenName);
        }

        foreach (GameStatus.visitingCharactersToSerialize visitedCharacter in loadedData.visitingCharacters)
        {
            Character characterToLoad = allCharactersToLoad.Find(character => character.characterName == visitedCharacter.characterName);
            characterToLoad.NumberOfTimesVisited = visitedCharacter.NumberOfTimesVisited;
            characterToLoad.lastVisitDateTimeString = visitedCharacter.lastVisitTime;

            foreach (string loadedAttractedItem in visitedCharacter.attractedItems)
            {
                Item attractedItemToLoad = allItemsToLoad.Find(item => item.ItemName == loadedAttractedItem);
                characterToLoad.AddToItemsAttractedBy(attractedItemToLoad);
            }

            characterToLoad.picturesTakenNameForCharacter.Clear();
            foreach (string pictureNamesForCharacter in visitedCharacter.picturesTakenNamesForCharacter)
            {

                characterToLoad.picturesTakenNameForCharacter.Add(pictureNamesForCharacter);

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

    private void AddCharactersSinceLastVisit1(Item setItem1, int NumberOfVisits)
    {
        for (int visitNum = 0; visitNum < NumberOfVisits; visitNum++)
        {
            characterToAdd = gameStatus.setItem1.GetAttractedCharacter();
            ++characterToAdd.RecentVisits;
            AddToRecentCharacterVistors(characterToAdd);
            if (!visitedCharacters.Contains(characterToAdd))
            {
                characterToAdd.AddToItemsAttractedBy(setItem1);
                characterToAdd.AddToVisits();
                characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
                gameStatus.AddToVisitedCharacters(characterToAdd);
                        }
            else if (visitedCharacters.Contains(characterToAdd))
            {
                Character CharacterToUpdate = visitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
                List<string> ItemListToCheck = CharacterToUpdate.ReturnItemNames();
                if (!ItemListToCheck.Contains(setItem1.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setItem1.ItemName);
                    gameStatus.AddItemToVisitedCharacter(characterToAdd, setItem1);
                      }
                else if (ItemListToCheck.Contains(setItem1.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setItem1.ItemName);
                    gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                }
            }
        }
    }

    private void AddCharactersSinceLastVisit2(Item setItem2, int NumberOfVisits)
    {
        for (int visitNum = 0; visitNum < NumberOfVisits; visitNum++)
        {
            characterToAdd = gameStatus.setItem2.GetAttractedCharacter();
            ++characterToAdd.RecentVisits;
            AddToRecentCharacterVistors(characterToAdd);
            if (!visitedCharacters.Contains(characterToAdd))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setItem2.ItemName);
                characterToAdd.AddToItemsAttractedBy(setItem2);
                characterToAdd.AddToVisits();
                characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
                gameStatus.AddToVisitedCharacters(characterToAdd);
               }
            else if (visitedCharacters.Contains(characterToAdd))
            {
                Character CharacterToUpdate = visitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
                List<string> ItemListToCheck = CharacterToUpdate.ReturnItemNames();
                if (!ItemListToCheck.Contains(setItem2.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setItem2.ItemName);
                    gameStatus.AddItemToVisitedCharacter(characterToAdd, setItem2);
                     }
                else if (ItemListToCheck.Contains(setItem2.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setItem2.ItemName);
                    gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                }
            }
        }
    }

    private void AddCharactersSinceLastVisit3(Item setItem3, int NumberOfVisits)
    {
        for (int visitNum = 0; visitNum < NumberOfVisits; visitNum++)
        {
            characterToAdd = gameStatus.setItem3.GetAttractedCharacter();
            ++characterToAdd.RecentVisits;
            AddToRecentCharacterVistors(characterToAdd);
            if (!visitedCharacters.Contains(characterToAdd))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setItem3.ItemName);
                characterToAdd.AddToItemsAttractedBy(setItem3);
                characterToAdd.AddToVisits();
                characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
                gameStatus.AddToVisitedCharacters(characterToAdd);
               }
            else if (visitedCharacters.Contains(characterToAdd))
            {
                Character CharacterToUpdate = visitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
                List<string> ItemListToCheck = CharacterToUpdate.ReturnItemNames();
                if (!ItemListToCheck.Contains(setItem3.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setItem3.ItemName);
                    gameStatus.AddItemToVisitedCharacter(characterToAdd, setItem3);
                }
                else if (ItemListToCheck.Contains(setItem3.ItemName))
                {
                    Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setItem3.ItemName);
                    gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                }
            }
        }
    }

    private void AddToRecentCharacterVistors(Character characterToAdd)
    {
        if (!recentVisitedCharacters.Contains(characterToAdd))
        {
            recentVisitedCharacters.Add(characterToAdd);
        }
        else
        {
            Character characterToUpdate = recentVisitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
            recentVisitedCharacters.Remove(characterToAdd);
            characterToUpdate.NumberOfTimesVisited++;
            recentVisitedCharacters.Add(characterToUpdate);
        }
    }
}
