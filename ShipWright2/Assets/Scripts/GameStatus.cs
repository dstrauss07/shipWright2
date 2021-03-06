﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameStatus : MonoBehaviour
{
    [Header("Constant Values to Be Reloaded")]
    [SerializeField] public List<Item> gameItems;
    [SerializeField] public List<Character> visitingCharacters;
    [SerializeField] public Item setItem1;
    [SerializeField] public Item setItem2;
    [SerializeField] public Item setItem3;
    [SerializeField] List<string> picturesTakenNames;

    [Header("Game Screen Setters")]
    public bool setModeActive = false;
    public bool pictureModeActive = false;
    public Item itemToSet;



    [Header("Times to Wait Between Loads")]
    [SerializeField] public float characterWaitMinTime = 3f;
    [SerializeField] public float characterWaitMaxTime = 15f;
    [SerializeField] public float timeBeforeRemoveMin = 5f;
    [SerializeField] public float timeBeforeRemoveMax = 20f;
    [SerializeField] public int timeBetweenVisitsWhenAway = 1;



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



    [Serializable]
    public class GameData
    {
        public List<string> PurchasedItems;
        public string setItem1Name;
        public string setItem2Name;
        public string setItem3Name;
        public List<string> picturesTaken;
        public List<visitingCharactersToSerialize> visitingCharacters;
        public string lastSaveTime;
    }
    [Serializable]
    public class visitingCharactersToSerialize
    {
        public string characterName;
        public int NumberOfTimesVisited;
        public List<string> attractedItems;
        public string lastVisitTime;
        public List<string> picturesTakenNamesForCharacter;
    }



    //writing file to persistent path C:\Users\Dave\AppData\LocalLow\DefaultCompany\ShipWright2


    public void Save()
    {
        Debug.Log("Game Saved");


        GameData data = new GameData();
        if (setItem1 != null) { data.setItem1Name = setItem1.ItemName; }
        if (setItem2 != null) { data.setItem2Name = setItem2.ItemName; }
        if (setItem3 != null) { data.setItem3Name = setItem3.ItemName; }
        data.PurchasedItems = new List<string>();

        for (int itemNum = 0; itemNum < gameItems.Count; itemNum++)
        {
            data.PurchasedItems.Add(gameItems[itemNum].ItemName);
            Debug.Log(gameItems[itemNum].ItemName + "added");
        }

        data.picturesTaken = new List<string>();

        for (int pictureNum = 0; pictureNum < picturesTakenNames.Count; pictureNum++)
        {
            data.picturesTaken.Add(picturesTakenNames[pictureNum]);
        }

        data.visitingCharacters = new List<visitingCharactersToSerialize>();

        for (int charNum = 0; charNum < visitingCharacters.Count; charNum++)
        {
            Debug.Log("charNum=" + charNum);
            Debug.Log("GameStatus:" + visitingCharacters[charNum].characterName + " " + visitingCharacters[charNum].NumberOfTimesVisited);
            visitingCharactersToSerialize visitingCharacterstoSerializeYo = new visitingCharactersToSerialize();

            visitingCharacterstoSerializeYo.characterName = visitingCharacters[charNum].characterName;
            visitingCharacterstoSerializeYo.NumberOfTimesVisited = visitingCharacters[charNum].NumberOfTimesVisited;
            visitingCharacterstoSerializeYo.lastVisitTime = visitingCharacters[charNum].lastVisitDateTimeString;
            visitingCharacterstoSerializeYo.attractedItems = new List<string>();

            for (int attractedItemsNum = 0; attractedItemsNum < visitingCharacters[charNum].ItemsAttractedBy.Count; attractedItemsNum++)
            {
                string ItemToAdd = visitingCharacters[charNum].ItemsAttractedBy[attractedItemsNum].ItemName;
                visitingCharacterstoSerializeYo.attractedItems.Add(ItemToAdd);

            }
            visitingCharacterstoSerializeYo.picturesTakenNamesForCharacter = new List<string>();
            for (int pictureNum = 0; pictureNum < visitingCharacters[charNum].picturesTakenNameForCharacter.Count; pictureNum++)
            {
                string pictureToAdd = visitingCharacters[charNum].picturesTakenNameForCharacter[pictureNum];
                visitingCharacterstoSerializeYo.picturesTakenNamesForCharacter.Add(pictureToAdd);
            }


            data.visitingCharacters.Add(visitingCharacterstoSerializeYo);
        }

        data.lastSaveTime = DateTime.Now.ToString();



        string dataPath = Path.Combine(Application.persistentDataPath, "gameData.json");
        string jsonGameData = JsonUtility.ToJson(data, true);
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            streamWriter.Write(jsonGameData);
        }
    }

    public GameData LoadGameData()
    {
        GameData data = new GameData();
        string dataPath = Path.Combine(Application.persistentDataPath, "gameData.json");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            string jsonString = streamReader.ReadToEnd();
            data = JsonUtility.FromJson<GameData>(jsonString);
            return data;
        }
    }

    public List<Item> ListGameItems()
    {
        return gameItems;
    }
    
    public void AddToGameItems(Item item)
    {
        gameItems.Add(item);
    }
        
    public void SetGameItemToSet(Item item)
    {
        itemToSet = item;
        itemToSet.itemIsInGameScreen = true;
    }
       
    public void SetItem1(Item item)
    {
        setItem1 = item;
    }

    public void SetItem2(Item item)
    {
        setItem2 = item;
    }

    public void SetItem3(Item item)
    {
        setItem3 = item;
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
        visitingCharacterToUpdate.lastVisitDateTimeString = DateTime.Now.ToString();
        visitingCharacters.Add(visitingCharacterToUpdate);
    }

    public void AddToCharacterVisitsOnly(Character visitedCharacter)
    {
        Character visitingCharacterToUpdate = visitingCharacters.Find(c => c.characterName == visitedCharacter.characterName);
        visitingCharacters.Remove(visitingCharacterToUpdate);
        visitingCharacterToUpdate.NumberOfTimesVisited++;
        visitingCharacterToUpdate.lastVisitDateTimeString = DateTime.Now.ToString();
        visitingCharacters.Add(visitingCharacterToUpdate);
    }

    public float GetTimeBeforeRemove()
    {
        float timeBeforeRemove = UnityEngine.Random.Range(timeBeforeRemoveMin, timeBeforeRemoveMax);
        return timeBeforeRemove;
    }


    public void AddAPicture(string characterName, string itemName)
    {
        if (!picturesTakenNames.Contains(characterName + itemName))
        {
            picturesTakenNames.Add(characterName + itemName);
            Character characterToAddPicture = visitingCharacters.Find(c => c.characterName == characterName);
            if(!characterToAddPicture.picturesTakenNameForCharacter.Contains(characterName + itemName))
            { characterToAddPicture.picturesTakenNameForCharacter.Add(characterName + itemName); }
           
            Save();
        }
    }

    public void AddAPicture(string pictureTakenName)
    {
        picturesTakenNames.Add(pictureTakenName);
    }

    public void QuitGame()
    {
        Save();
        Application.Quit();
    }


}
