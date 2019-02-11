using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestPanel : MonoBehaviour
{

    [SerializeField] List<Character> allCharacters;
    [SerializeField] List<Character> visitedCharacters; //for debugging


    [SerializeField] TextMeshProUGUI pageText;
    [SerializeField] List<GameObject> menuItems;
    [SerializeField] List<GameObject> MenuItemImages;
    [SerializeField] List<TextMeshProUGUI> MenuItemNames;
    [SerializeField] List<TextMeshProUGUI> attractedByItems;
    [SerializeField] List<TextMeshProUGUI> MenuItemVisits;

    int page = 1;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        visitedCharacters = gameStatus.GetListVisitingCharacters();
        ClearPreviousPageItems();
        PopulateMenu();
    }

    private void PopulateMenu()
    {
        TogglePageButtons();
        removeUnnecessaryItemSpots();
        pageText.text = "Page " + page.ToString() + " Of " + GetPageCount().ToString();
        PopulateVisitedCharactersInfo();
    }

    private void PopulateVisitedCharactersInfo()
    {
        if (GetPageCount() == 1)
        {
            for (int itemNum = 0; itemNum < itemsListLength(); itemNum++)
            {
                var currentCharacter = allCharacters[itemNum];
               //    var  listOfItems =   currentCharacter.ReturnItemsAttractedBy();
                if (visitedCharacters.Contains(currentCharacter))
                {
                    string currentItemName = currentCharacter.characterName;
                    string currentItemVisits = currentCharacter.NumberOfTimesVisited.ToString();
                    var currentItemSprite = currentCharacter.GetComponent<SpriteRenderer>().sprite;
                  //  var currentItemAttract = listOfItems[0].ItemName;
                    MenuItemNames[itemNum].text = currentItemName;
                    MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
                    MenuItemVisits[0].text = currentItemVisits;
                    //attractedByItems[0].text = currentItemAttract;
                }
            }
        }
        else if (page != GetPageCount())
        {
            for (int itemNum = 0; itemNum < 4; itemNum++)
            {
                var currentCharacter = allCharacters[itemNum + ((page - 1) * 4)];
                var listOfItems = currentCharacter.ReturnItemsAttractedBy();
                if (visitedCharacters.Contains(currentCharacter))
                {
                    string currentItemName = currentCharacter.characterName;
                    string currentItemVisits = currentCharacter.NumberOfTimesVisited.ToString();
                    var currentItemSprite = currentCharacter.GetComponent<SpriteRenderer>().sprite;
                 //       var currentItemAttract = listOfItems[0].ItemName;
                    MenuItemNames[itemNum].text = currentItemName;
                    MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
                    MenuItemVisits[0].text = currentItemVisits;
                //    attractedByItems[0].text = currentItemAttract;
                }
            }
        }
        else
        {
            for (int itemNum = 4 - itemListRemainder(); itemNum < 4; itemNum++)
            {
                int itemIndex = 0;
                var currentCharacter = allCharacters[itemIndex + ((page - 1) * 4)];
                var listOfItems = currentCharacter.ReturnItemsAttractedBy();
                if (visitedCharacters.Contains(currentCharacter))
                {
                    string currentItemName = currentCharacter.characterName;
                    string currentItemVisits = currentCharacter.NumberOfTimesVisited.ToString();
                    var currentItemSprite = currentCharacter.GetComponent<SpriteRenderer>().sprite;
                    //   var currentItemAttract = listOfItems[0].ItemName;
                    MenuItemNames[itemIndex].text = currentItemName;
                    MenuItemImages[itemIndex].GetComponent<Image>().sprite = currentItemSprite;
                    MenuItemVisits[0].text = currentItemVisits;
                   // attractedByItems[0].text = currentItemAttract;
                    itemIndex++;
                }
            }
        }
    }

    private void removeUnnecessaryItemSpots()
    {
        if (page == GetPageCount() && itemListRemainder() != 0)

        {
            for (int itemNum = itemListRemainder(); itemNum < 4; itemNum++)
            {
                menuItems[itemNum].SetActive(false);
            }
        }
        else
        {
            for (int itemNum = itemListRemainder(); itemNum < 4; itemNum++)
            {
                menuItems[itemNum].SetActive(true);
            }
        }
    }

    public int itemsListLength()
    {
        return allCharacters.Count;
    }

    public int itemListRemainder()
    {
        return itemsListLength() % 4;
    }

    public int GetPageCount()
    {

        int records = itemsListLength();
        int pageCount = (records + 3) / 4;
        return pageCount;
    }

    private void TogglePageButtons()
    {
        if (page == 1)
        {
            GameObject.Find("Previous Page").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Previous Page").GetComponent<Button>().enabled = false;
        }
        else
        {
            GameObject.Find("Previous Page").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Previous Page").GetComponent<Button>().enabled = true;
        }

        if (page == GetPageCount())
        {
            GameObject.Find("Next Page").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Next Page").GetComponent<Button>().enabled = false;
        }
        else
        {
            GameObject.Find("Next Page").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Next Page").GetComponent<Button>().enabled = true;
        }
    }

    public void NextPage()
    {
        page++;
        ClearPreviousPageItems();
        PopulateMenu();
    }

    public void PreviousPage()
    {
        page--;
        ClearPreviousPageItems();
        PopulateMenu();
    }

    private void ClearPreviousPageItems()
    {
        for (int menuIndex = 0; menuIndex < 4; menuIndex++)
        {
            menuItems[menuIndex].GetComponentInChildren<TextMeshProUGUI>().text = "????";
            menuItems[menuIndex].GetComponentInChildren<Image>().sprite = null;
        }
    }
}
