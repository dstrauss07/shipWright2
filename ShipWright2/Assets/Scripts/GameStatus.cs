using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
   [SerializeField] List<Item> gameItems;


    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameStatus>().Length;

        if (numberGameSessions > 1)
        {
            Debug.Log("destroyed game session");
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


}
