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


    int page = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        inventoryItems = gameStatus.ListGameItems();
        if (inventoryItems.Count > 0)
        {
            TogglePageButtons();
            PopulateItemList();
        }
        else
        {
            GameObject.Find("InventoryPanel").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       pageText.text = "Page " + page.ToString() + " Of " + GetPageCount().ToString();


    }

    private void PopulateItemList()
    {
        for (int itemNum = 0; itemNum < 4; itemNum++)
        {
            var currentItem = inventoryItems[itemNum + ((page - 1) * 4)];
            string currentItemName = currentItem.ItemName;
            string currentItemCost = currentItem.ItemCost.ToString();
            var currentItemSprite = currentItem.GetComponent<SpriteRenderer>().sprite;
            MenuItemNames[itemNum].text = currentItemName;
            MenuItemImages[itemNum].GetComponent<Image>().sprite = currentItemSprite;
        }
    }


    public void NextPage()
    {
        Debug.Log("Next Page!");
        page++;
    }


    public void PreviousPage()
    {
        Debug.Log("Previus Page!");
        page--;
    }


    public int itemsListLength()
    {
        return inventoryItems.Count;
    }

    public int GetPageCount()
    {
        InventoryPanel inventoryPanel = GetComponent<InventoryPanel>();
        int records = inventoryPanel.itemsListLength();
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
