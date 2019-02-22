using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public List<Item> ItemsAttractedBy;
    public string CurrentItemAttractString;
    [SerializeField] public int NumberOfTimesVisited = 0;
    [SerializeField] public string characterName;
    public List<string> ItemListNamesToReturn;
    public string lastVisitDateTimeString;
    public List<string> picturesTakenNameForCharacter;
    [SerializeField] public Sprite characterSprite;


    public void AddToItemsAttractedBy(Item itemAttractedBy)
    {
        if (!ItemsAttractedBy.Contains(itemAttractedBy))
        {
            ItemsAttractedBy.Add(itemAttractedBy);
        }
    }

    public List<Item> ReturnItemsAttractedBy()
    {
        return ItemsAttractedBy;
    }

    public List<string> ReturnItemNames()
    {
        
        for (int i=0; i < ItemsAttractedBy.Count; i++)
        {
            ItemListNamesToReturn.Add(ItemsAttractedBy[i].ItemName);
        }
        return ItemListNamesToReturn;

    }

    public string ReturnCurrentItemAttractString()
    {
        Debug.Log(ItemsAttractedBy.Count + "Items in attracted List");
        if (ItemsAttractedBy == null || ItemsAttractedBy.Count == 0)
        {
            CurrentItemAttractString = "????" + "\n" + "????" + "\n" + "????";
            return CurrentItemAttractString;
        }
        if (ItemsAttractedBy.Count == 1)
        {
            CurrentItemAttractString = ItemsAttractedBy[0].ItemName + "\n" + "????" + "\n" + "????";
            return CurrentItemAttractString;
        }
        if (ItemsAttractedBy.Count == 2)
        {
            CurrentItemAttractString = ItemsAttractedBy[0].ItemName + "\n" + ItemsAttractedBy[1].ItemName + "\n" + "????";
            return CurrentItemAttractString;
        }
        if (ItemsAttractedBy.Count == 3)
        {
            CurrentItemAttractString = ItemsAttractedBy[0].ItemName + "\n" + ItemsAttractedBy[1].ItemName + "\n" + ItemsAttractedBy[2].ItemName;
            return CurrentItemAttractString;
        }
        else
        {
            Debug.Log("issue with the count of attracted Items");
            return "something broke";
        }

    }



    public void AddToVisits()
    {
        NumberOfTimesVisited++;
    }

    public int ReturnNumberOfVisits()
    {
        return NumberOfTimesVisited;
    }

}
