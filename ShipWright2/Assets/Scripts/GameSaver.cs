using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaver : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameStatus _gameStatus;
    string dataPath = Path.Combine(Application.persistentDataPath, "gameData.txt");
    static List<Item> saveGameItems = _gameStatus.ListGameItems();
    static List<Character> saveGameCharacters = _gameStatus.GetListVisitingCharacters();
    static Item saveSetItem1 = _gameStatus.getSetItem1();


    void Start()
    {
       if (!File.Exists(dataPath))
        {
            File.Create(dataPath);
            Debug.Log("Json File Created");
        }
    }



    public static void SaveGame()
    {
       string dataPath = Path.Combine(Application.persistentDataPath, "gameData.txt");
       string jsonGameItems = JsonUtility.ToJson(saveGameItems);
       string jsonCharacters = JsonUtility.ToJson(saveGameCharacters);
       string jsonSetItem = JsonUtility.ToJson(saveSetItem1);

        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            streamWriter.Write(jsonGameItems);
            streamWriter.Write(jsonCharacters);
            streamWriter.Write(jsonSetItem);
        }

    }

    //public static void LoadGame()
    //{
    //    string dataPath = Path.Combine(Application.persistentDataPath, "gameData.txt");
    //    using (StreamReader streamReader = File.OpenText(dataPath))
    //    {
    //        string jsonString = streamReader.ReadToEnd();
    //        return JsonUtility.FromJson<CharacterData>(jsonString);
    //    }
    //}





}
