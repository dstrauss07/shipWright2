using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] List<Item> ItemsAttractedBy;
    [SerializeField] int NumberOfTimesVisited = 0;


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
