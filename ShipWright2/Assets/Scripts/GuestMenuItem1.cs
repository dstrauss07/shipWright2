using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestMenuItem1 : MonoBehaviour
{
    [SerializeField] GameObject MainImage;
    [SerializeField] TextMeshProUGUI CharacterName;
    [SerializeField] TextMeshProUGUI visitNumber;
    [SerializeField] TextMeshProUGUI lastVisit;
    [SerializeField] TextMeshProUGUI attractedByItems;
    [SerializeField] List<Image> CharacterPictures;
    [SerializeField] PictureSprite mysteryImage;

    public void SetMainImage(Sprite mainImageSprite)
    {
        MainImage.GetComponent<Image>().sprite = mainImageSprite;
    }

    public void SetCharacterName(string characterName)
    {
        CharacterName.text = characterName;
    }

    public void SetVisitNumber(string visitNum)
    {
        visitNumber.text = visitNum;
    }

    public void SetLastVisit(string lastVisitNum)
    {
        lastVisit.text = lastVisitNum;
    }

    public void SetAttractedItems(string attractedItems)
    {
        attractedByItems.text = attractedItems;
    }

    public void SetCharacterPictures(List<PictureSprite> charPictures)
    {
        for (int pictureNum = 0; pictureNum < 3; pictureNum++)
        {
            if (charPictures[pictureNum] != null)
            {
                CharacterPictures[pictureNum].GetComponent<Image>().sprite = charPictures[pictureNum].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                CharacterPictures[pictureNum].GetComponent<Image>().sprite = mysteryImage.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    public void SetCharacterPictures()
    {
        for (int pictureNum = 0; pictureNum < 3; pictureNum++)
        {

            CharacterPictures[pictureNum].GetComponent<Image>().sprite = mysteryImage.GetComponent<SpriteRenderer>().sprite;

        }
    }
}
