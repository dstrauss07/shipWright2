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

        int NumberOfVisits = CalculateNumberOfVisitsSinceLastSave();
        AddCharactersSinceLastVisit(setItem1, NumberOfVisits);

        recentVisitorPanel = FindObjectOfType<RecentVisitorsPanel>().gameObject;

        if (recentVisitedCharacters.Count == 0 || recentVisitedCharacters==null)
        {
            Debug.Log("hiding Recent Visitors Panel");
            recentVisitorPanel.SetActive(false);
        }

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
            AddToRecentCharacterVistors(characterToAdd);
            if (!visitedCharacters.Contains(characterToAdd))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setItem1.ItemName);
                characterToAdd.AddToItemsAttractedBy(setItem1);
                characterToAdd.AddToVisits();
                characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
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
