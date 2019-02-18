using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    GameStatus gameStatus;
    

    [SerializeField] List<Item> inventoryItems;
    [SerializeField] List<TextMeshProUGUI> MenuItemNames;
    [SerializeField] List<GameObject> MenuItemImages;
    [SerializeField] TextMeshProUGUI pageText;
    [SerializeField] List<GameObject> menuItems;

    int page = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        inventoryItems = gameStatus.ListGameItems();
        if (inventoryItems.Count == 0)
        {
            GameObject.Find("InventoryPanel").SetActive(false);
        }
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
            for (int itemNum = 0; itemNum < 4; itemNum++)
            {
                Debug.Log("not final page # " + page + "itemNum" + itemNum);
                var currentItem = inventoryItems[itemNum + ((page - 1) * 4)];
                string currentItemName = currentItem.ItemName;
                var currentItemSprite = currentItem.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentItemName;
                MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
            }
        }
        else if (page == GetPageCount() && itemListRemainder() !=0)
        {
            for (int itemNum = 0;  itemNum < itemListRemainder(); itemNum++)
            {
                Debug.Log("final page # " + page + "itemNum" + itemNum);
                var currentItem = inventoryItems[itemNum + ((page - 1) * 4)];
                string currentItemName = currentItem.ItemName;
                var currentItemSprite = currentItem.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentItemName;
                MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
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
        return inventoryItems.Count;
    }

    public int GetPageCount()
    {
        int records = itemsListLength();
        int pageCount = (records + 3) / 4;
        return pageCount;
    }

    public int itemListRemainder()
    {
        return itemsListLength() % 4;
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


    public void SetGameItem0()
    {
        gameStatus.SetGameItemToSet(inventoryItems[0 + ((page - 1) * 4)]);
        SceneManager.LoadScene("PlayScreen");
        gameStatus.setModeActive = true;
    }

    public void SetGameItem1()
    {
        gameStatus.SetGameItemToSet(inventoryItems[1 + ((page - 1) * 4)]);
        SceneManager.LoadScene("PlayScreen");
        gameStatus.setModeActive = true;
    }

    public void SetGameItem2()
    {
        gameStatus.SetGameItemToSet(inventoryItems[2 + ((page - 1) * 4)]);
        SceneManager.LoadScene("PlayScreen");
        gameStatus.setModeActive = true;
    }

    public void SetGameItem3()
    {
        gameStatus.SetGameItemToSet(inventoryItems[3 + ((page - 1) * 4)]);
        SceneManager.LoadScene("PlayScreen");
        gameStatus.setModeActive = true;
    }

}
