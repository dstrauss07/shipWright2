﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
   [SerializeField] List<Item> gameItems;  //for Debugging
   [SerializeField] Item itemToSet;
   [SerializeField] Item setItem1;
   public bool setModeActive = false;
   Character attractedCharacter1;


    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        attractedCharacter1 = setItem1.GetAttractedCharacter1();
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

    public Character GetAttractedCharacter1()
    {
        return attractedCharacter1;
    }



}
