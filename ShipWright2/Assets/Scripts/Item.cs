using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{

    [SerializeField] public string ItemName;
    [SerializeField] public int ItemCost = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName()
    {
        //TextMeshProUGUI ItemNameTmPro = GetComponent<TextMeshProUGUI>();
        //ItemNameTmPro.SetText(ItemName);
        return ItemName;
    }

    //public string GetItemName()
    //{
    //    return ItemName;
    //}
}
