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
    [SerializeField] PictureSprite mysteryImage;
    [SerializeField] List<PictureSprite> characterPictures;
    [SerializeField] GuestMenuItem menuItem0;
    [SerializeField] GuestMenuItem1 menuItem1;
    List<PictureSprite> pictureSpriteList;

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
        pageText.text = "Page " + page.ToString() + " Of " + GetPageCount().ToString();
        PopulateVisitedCharactersInfo();
    }

    private void PopulateVisitedCharactersInfo()
    {
        for (int itemNum = 0; itemNum < 2; itemNum++)
        {

            var currentCharacter = allCharacters[itemNum + ((page - 1) * 2)];
            if (itemNum == 0)
            {
                if (visitedCharacters.Contains(currentCharacter))
                {
                    PopulateMenuItem0(currentCharacter);
                }
                else
                {
                    MenuItem0Blank();
                }
            }

            if (itemNum == 1)
            {
                if (visitedCharacters.Contains(currentCharacter))
                {

                    PopulateMenuItem1(currentCharacter);
                }
                else
                {
                    MenuItem1Blank();
                }
            }

        }
    }

    public int itemsListLength()
    {
        return allCharacters.Count;
    }

    public int itemListRemainder()
    {
        return itemsListLength() % 2;
    }

    public int GetPageCount()
    {

        int records = itemsListLength();
        int pageCount = (records + 1) / 2;
        return pageCount;
    }

    private void TogglePageButtons()
    {
        if (page == 1)
        {
            GameObject.Find("Previous Page").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Previous Page").GetComponent<Button>().enabled = false;
        }
        else if (page == GetPageCount())
        {
            GameObject.Find("Next Page").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Next Page").GetComponent<Button>().enabled = false;
        }

        else
        {
            GameObject.Find("Next Page").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Next Page").GetComponent<Button>().enabled = true;
            GameObject.Find("Previous Page").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Previous Page").GetComponent<Button>().enabled = true;
        }
    }

    private void ClearPreviousPageItems()
    {
        MenuItem0Blank();
        MenuItem1Blank();

    }


    private void MenuItem0Blank()
    {
        menuItem0.SetMainImage(mysteryImage.GetComponent<SpriteRenderer>().sprite);
        menuItem0.SetCharacterName("???????");
        menuItem0.SetVisitNumber("0");
        menuItem0.SetLastVisit("Never Visited");
        menuItem0.SetAttractedItems("????" + "\n" + "????" + "\n" + "????");
        menuItem0.SetCharacterPictures();
    }

    private void MenuItem1Blank()
    {
        menuItem1.SetMainImage(mysteryImage.GetComponent<SpriteRenderer>().sprite);
        menuItem1.SetCharacterName("???????");
        menuItem1.SetVisitNumber("0");
        menuItem1.SetLastVisit("Never Visited");
        menuItem1.SetAttractedItems("????" + "\n" + "????" + "\n" + "????");
        menuItem1.SetCharacterPictures();
    }

    private void PopulateMenuItem0(Character currentCharacter)
    {
        menuItem0.SetMainImage(currentCharacter.characterSprite);
        menuItem0.SetCharacterName(currentCharacter.characterName);
        menuItem0.SetVisitNumber(currentCharacter.NumberOfTimesVisited.ToString());
        menuItem0.SetLastVisit(currentCharacter.lastVisitDateTimeString);
        menuItem0.SetAttractedItems(currentCharacter.ReturnCurrentItemAttractString());


        List<PictureSprite> pictureSpriteList = new List<PictureSprite>();
        foreach (string pictureName in currentCharacter.picturesTakenNameForCharacter)
        {
            PictureSprite pictureSpriteToAdd = characterPictures.Find(p => p.pictureSpriteName == pictureName);
            pictureSpriteList.Add(pictureSpriteToAdd);
        }

        menuItem0.SetCharacterPictures(pictureSpriteList);
    }


    private void PopulateMenuItem1(Character currentCharacter)
    {
        menuItem1.SetMainImage(currentCharacter.characterSprite);
        menuItem1.SetCharacterName(currentCharacter.characterName);
        menuItem1.SetVisitNumber(currentCharacter.NumberOfTimesVisited.ToString());
        menuItem1.SetLastVisit(currentCharacter.lastVisitDateTimeString);
        menuItem1.SetAttractedItems(currentCharacter.ReturnCurrentItemAttractString());

        List<PictureSprite> pictureSpriteList = new List<PictureSprite>();
        foreach(string pictureName in currentCharacter.picturesTakenNameForCharacter)
        {
            PictureSprite pictureSpriteToAdd = characterPictures.Find(p => p.pictureSpriteName == pictureName);
            pictureSpriteList.Add(pictureSpriteToAdd);
        }
               
        menuItem1.SetCharacterPictures(pictureSpriteList);
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


}
