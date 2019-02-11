using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] public List<Item> ItemsAttractedBy;
    [SerializeField] public int NumberOfTimesVisited = 0;
    [SerializeField] public string characterName;


    public void AddToItemsAttractedBy(Item itemAttractedBy)
    {
        if(!ItemsAttractedBy.Contains(itemAttractedBy))
        {
            ItemsAttractedBy.Add(itemAttractedBy);
        }
    }

    public List<Item> ReturnItemsAttractedBy()
    {
        return ItemsAttractedBy;
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
