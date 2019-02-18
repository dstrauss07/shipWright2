using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameStatus : MonoBehaviour
{
    [Header("Constant Values to Be Reloaded")]
    [SerializeField] List<Item> gameItems;
    [SerializeField] List<Character> visitingCharacters;
    [SerializeField] public Item setItem1;

    [Header("Item Setters")]
    Item itemToSet;
    public bool setModeActive = false;


    [Header("Times to Wait Between Loads")]
    [SerializeField] float characterWaitTime = 5f;
    [SerializeField] float timeBeforeRemove = 15f;
   [SerializeField] public int timeBetweenVisitsWhenAway = 5;

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
        public List<visitingCharactersToSerialize> visitingCharacters;
        public string setItem1Name;
        public string lastSaveTime;
    }
    [Serializable]
    public class visitingCharactersToSerialize
    {
        public string characterName;
        public int NumberOfTimesVisited;
        public List<string> attractedItems;
    }



    //writing file to persistent path C:\Users\Dave\AppData\LocalLow\DefaultCompany\ShipWright2


    public void Save()
    {
        Debug.Log("Game Saved");


        GameData data = new GameData();
        if (setItem1 != null) { data.setItem1Name = setItem1.ItemName; }
        data.PurchasedItems = new List<string>();

        for (int itemNum = 0; itemNum < gameItems.Count; itemNum++)
        {
            data.PurchasedItems.Add(gameItems[itemNum].ItemName);
            Debug.Log(gameItems[itemNum].ItemName + "added");
        }

        data.visitingCharacters = new List<visitingCharactersToSerialize>();

        for (int charNum = 0; charNum < visitingCharacters.Count; charNum++)
        {
            Debug.Log("charNum=" + charNum);
            Debug.Log("GameStatus:" + visitingCharacters[charNum].characterName + " " + visitingCharacters[charNum].NumberOfTimesVisited);
            visitingCharactersToSerialize visitingCharacterstoSerializeYo = new visitingCharactersToSerialize();

            visitingCharacterstoSerializeYo.characterName = visitingCharacters[charNum].characterName;
            visitingCharacterstoSerializeYo.NumberOfTimesVisited = visitingCharacters[charNum].NumberOfTimesVisited;

            visitingCharacterstoSerializeYo.attractedItems = new List<string>();

            for (int attractedItemsNum = 0; attractedItemsNum < visitingCharacters[charNum].ItemsAttractedBy.Count; attractedItemsNum++)
            {
                string ItemToAdd = visitingCharacters[charNum].ItemsAttractedBy[attractedItemsNum].ItemName;
                visitingCharacterstoSerializeYo.attractedItems.Add(ItemToAdd);

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

    public float GetTimeBeforeRemove()
    {
        return timeBeforeRemove;
    }

    public float GetTimeBeforeSpawn()
    {
        return characterWaitTime;
    }



}
