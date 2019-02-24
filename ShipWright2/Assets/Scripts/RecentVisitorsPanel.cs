using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecentVisitorsPanel : MonoBehaviour
{

    StartScreenManager startScreenManager;


    [SerializeField] List<TextMeshProUGUI> MenuItemNames;
    [SerializeField] List<GameObject> MenuItemImages;
    [SerializeField] TextMeshProUGUI pageText;
    [SerializeField] List<GameObject> menuItems;
    [SerializeField] List<TextMeshProUGUI> menuVisits;
    [SerializeField] List<Character> recentVisitedCharacters;


    int page = 1;

    //// Start is called before the first frame update
    void Start()
    {
        startScreenManager = FindObjectOfType<StartScreenManager>();
        recentVisitedCharacters = startScreenManager.recentVisitedCharacters;
        PopulateMenu();
    }





    private void PopulateMenu()
    {
        pageText.text = "Page " + page.ToString() + " Of " + GetPageCount().ToString();
        TogglePageButtons();
        removeUnnecessaryItemSpots();
        PopulateItemList();
    }

    private void PopulateItemList()
    {
        if (page != GetPageCount() || itemListRemainder() == 0)
        {
            for (int itemNum = 0; itemNum < 5; itemNum++)
            {
                Debug.Log("final page # " + page + "itemNum" + itemNum);
                var currentCharacter = recentVisitedCharacters[itemNum + ((page - 1) * 4)];
                string currentCharacterName = currentCharacter.characterName;
                string currentCharacterVisits = currentCharacter.NumberOfTimesVisited.ToString();
                var currentCharacterSprite = currentCharacter.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentCharacterName;
                MenuItemImages[itemNum].GetComponent<Image>().sprite = currentCharacterSprite;
                menuVisits[itemNum].text = currentCharacterVisits + " Visits";

            }
        }
        else if (page == GetPageCount() && itemListRemainder() != 0)
        {
            for (int itemNum = 0; itemNum < itemListRemainder(); itemNum++)
            {
                Debug.Log("final page # " + page + "itemNum" + itemNum);
                var currentCharacter = recentVisitedCharacters[itemNum + ((page - 1) * 4)];
                string currentCharacterName = currentCharacter.characterName;
                string currentCharacterVisits = currentCharacter.NumberOfTimesVisited.ToString();
                var currentCharacterSprite = currentCharacter.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentCharacterName;
                MenuItemImages[itemNum].GetComponent<Image>().sprite = currentCharacterSprite;
                menuVisits[itemNum].text = currentCharacterVisits + " Visits";
            }
        }
    }

    private void removeUnnecessaryItemSpots()
    {
        if (page == GetPageCount() && itemListRemainder() != 0)

        {
            for (int itemNum = itemListRemainder(); itemNum < 5; itemNum++)
            {
                menuItems[itemNum].SetActive(false);
            }
        }
        else
        {
            for (int itemNum = itemListRemainder(); itemNum < 5; itemNum++)
            {
                menuItems[itemNum].SetActive(true);
            }
        }
    }


    public int itemsListLength()
    {
        return recentVisitedCharacters.Count;
    }

    public int GetPageCount()
    {
        int records = itemsListLength();
        int pageCount = (records + 4) / 5;
        return pageCount;
    }

    public int itemListRemainder()
    {
        return itemsListLength() % 5;
    }


    public void NextPage()
    {
        Debug.Log("Next Page!");
        page++;
        PopulateMenu();
    }


    public void PreviousPage()
    {
        Debug.Log("Previus Page!");
        page--;
        PopulateMenu();
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


}
