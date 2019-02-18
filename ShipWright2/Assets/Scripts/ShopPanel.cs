using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopPanel : MonoBehaviour

{

    [SerializeField] List<Item> items;
    [SerializeField] List<TextMeshProUGUI> MenuItemNames;
    [SerializeField] List<TextMeshProUGUI> MenuItemCosts;
    [SerializeField] List<GameObject> MenuItemImages;
    [SerializeField] List<GameObject> menuItems;
    [SerializeField] TextMeshProUGUI pageText;


    int page = 1;
    GameStatus gameStatus;

    public void Start()
    {

        gameStatus = FindObjectOfType<GameStatus>();
        PopulateMenu();

    }


    private void PopulateMenu()
    {
        TogglePageButtons();
        removeUnnecessaryItemSpots();
        PopulateItemList();
        pageText.text = page.ToString() + " Of " + GetPageCount().ToString();
    }

    private void PopulateItemList()
    {

        if (page != GetPageCount() || itemListRemainder() == 0)
        {
            for (int itemNum = 0; itemNum < 4; itemNum++)
            {
                var currentItem = items[itemNum + ((page - 1) * 4)];
                string currentItemName = currentItem.ItemName;
                string currentItemCost = currentItem.ItemCost.ToString();
                var currentItemSprite = currentItem.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentItemName;
                MenuItemCosts[itemNum].text = currentItemCost;
                MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
            }
        }
        else if (page == GetPageCount() && itemListRemainder() != 0)
        {
            for (int itemNum = 0; itemNum < itemListRemainder(); itemNum++)
            {
                var currentItem = items[itemNum + ((page - 1) * 4)];
                string currentItemName = currentItem.ItemName;
                string currentItemCost = currentItem.ItemCost.ToString();
                var currentItemSprite = currentItem.GetComponent<SpriteRenderer>().sprite;
                MenuItemNames[itemNum].text = currentItemName;
                MenuItemCosts[itemNum].text = currentItemCost;
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
        return items.Count;
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

    public void AddToGameItems0()
    {
        gameStatus.AddToGameItems(items[0 + ((page - 1) * 4)]);
        gameStatus.Save();
        SceneManager.LoadScene("Inventory");
    }

    public void AddToGameItems1()
    {
        gameStatus.AddToGameItems(items[1 + ((page - 1) * 4)]);
        gameStatus.Save();
        SceneManager.LoadScene("Inventory");
    }

    public void AddToGameItems2()
    {
        gameStatus.AddToGameItems(items[2 + ((page - 1) * 4)]);
        gameStatus.Save();
        SceneManager.LoadScene("Inventory");
    }

    public void AddToGameItems3()
    {
        gameStatus.AddToGameItems(items[3 + ((page - 1) * 4)]);
        gameStatus.Save();
        SceneManager.LoadScene("Inventory");
    }

}
