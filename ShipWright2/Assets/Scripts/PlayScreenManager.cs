using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayScreenManager : MonoBehaviour
{

    GameStatus gameStatus;
    GameObject setButton;
    AnimationSpawner animationSpawner;
    GameObject setButtonText;
    Transform setButtonLocation;
    Item setGameItem;
    float characterWaitTime;
    Character characterToAdd;
    Item itemToAdd;
    List<Character> VisitedCharacters;
    GameObject target1;

    [SerializeField] AudioClip cameraSound;


    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        animationSpawner = FindObjectOfType<AnimationSpawner>();
        VisitedCharacters = gameStatus.GetListVisitingCharacters();
        characterWaitTime = gameStatus.GetTimeBeforeSpawn();
        SetButton1();
        toggleCamera();
        target1 = GameObject.Find("Target1");
        target1.GetComponent<Image>().enabled = false;
        target1.GetComponent<Button>().enabled = false;




    }
    // Update is called once per frame
    void Update()
    {
        if (setGameItem != null)
        {

            if (setGameItem.itemIsInGameScreen)
            {
                characterWaitTime -= Time.deltaTime;
                if (characterWaitTime <= 0)
                {
                    characterToAdd = setGameItem.GetAttractedCharacter();
                    SpawnAnimationForCharacterAndItem();
                    AddCharacterToVisitedCharacters();
                }
            }
        }
    }

    private void SpawnAnimationForCharacterAndItem()
    {
        AnimationScript animationToSpawn = animationSpawner.ReturnRequestedAnimation(characterToAdd, setGameItem);
        AnimationScript setAnimation = Instantiate(animationToSpawn, setButtonLocation.position, Quaternion.identity) as AnimationScript;
        setGameItem.itemIsInGameScreen = false;
        gameStatus.pictureModeActive = true;
        toggleCamera();
        StartCoroutine(WaitThenRemoveAnimation());
    }

    private IEnumerator WaitThenRemoveAnimation()
    {
        yield return new WaitForSeconds(gameStatus.GetTimeBeforeRemove());
        Destroy(gameObject);
        setGameItem.itemIsInGameScreen = true;
        gameStatus.pictureModeActive = false;
        SceneManager.LoadScene("PlayScreen");
    }

    private void AddCharacterToVisitedCharacters()
    {
        if (!VisitedCharacters.Contains(characterToAdd))
        {
            Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setGameItem.ItemName);
            characterToAdd.AddToItemsAttractedBy(setGameItem);
            characterToAdd.AddToVisits();
            characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
            gameStatus.AddToVisitedCharacters(characterToAdd);
            gameStatus.Save();
            characterWaitTime = 5000f;
        }
        else if (VisitedCharacters.Contains(characterToAdd))
        {
            var CharacterToUpdate = VisitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
            var ItemListToCheck = CharacterToUpdate.ReturnItemNames();
            if (!ItemListToCheck.Contains(setGameItem.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setGameItem.ItemName);
                gameStatus.AddItemToVisitedCharacter(characterToAdd, setGameItem);
                gameStatus.Save();
                characterWaitTime = 5000f;
            }
            else if (ItemListToCheck.Contains(setGameItem.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setGameItem.ItemName);
                gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                gameStatus.Save();
                characterWaitTime = 5000f;
            }
        }
    }

    private void SetButton1()
    {
        setButton = GameObject.Find("SetButton");
        setButtonText = GameObject.Find("SetButtonText");
        setButtonLocation = setButton.transform;

        if (!gameStatus.setModeActive)
        {
            HideSetArea();
        }

        if (gameStatus.getSetItem1() != null && !gameStatus.setModeActive)
        {
            setGameItem = Instantiate(gameStatus.getSetItem1(), setButtonLocation);
            setGameItem.transform.localScale += new Vector3(100f, 100f, 0);
            setGameItem.itemIsInGameScreen = true;
            HideSetArea();
        }

        if (gameStatus.getSetItem1() != null && gameStatus.setModeActive)
        {
            setGameItem = Instantiate(gameStatus.getSetItem1(), setButtonLocation);
            setGameItem.transform.localScale += new Vector3(75f, 75f, 0);
            Color currentImage = setGameItem.GetComponent<SpriteRenderer>().color;
            currentImage.a = 0.15f;
            setGameItem.GetComponent<SpriteRenderer>().color = currentImage;
            setGameItem.itemIsInGameScreen = false;
        }
    }


    public void setItemHere()
    {
        setGameItem = Instantiate(gameStatus.getItemToSet(), setButtonLocation);
        setGameItem.transform.localScale += new Vector3(100f, 100f, 0);
        gameStatus.setModeActive = false;
        gameStatus.SetItem1(gameStatus.getItemToSet());
        gameStatus.Save();
        SceneManager.LoadScene("PlayScreen");
    }

    private void HideSetArea()
    {
        setButton.GetComponent<Image>().enabled = false;
        setButton.GetComponent<Button>().enabled = false;
        setButtonText.GetComponent<Text>().enabled = false;
    }

    private void toggleCamera()
    {
        GameObject pictureTaker = GameObject.Find("PictureTaker");
        if(!gameStatus.pictureModeActive)
        {
            pictureTaker.GetComponent<Image>().enabled = false;
            pictureTaker.GetComponent<Button>().enabled = false;
        }
        else
        {
            pictureTaker.GetComponent<Image>().enabled = true;
            pictureTaker.GetComponent<Button>().enabled = true;
        }
    }

    public void turnOnTargets()
        {
        Debug.Log("take a picture");
        target1 = GameObject.Find("Target1");
        target1.GetComponent<Image>().enabled = true;
        target1.GetComponent<Button>().enabled = true;
        }

    public void TakeAPicture1()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem1.ItemName;
        string characterForPicture = characterToAdd.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target1));
        
    }

    private IEnumerator Flash(GameObject target)
    {
        GameObject thisTarget = target;
        AudioSource.PlayClipAtPoint(cameraSound, Camera.main.transform.position);
        thisTarget.GetComponent<Image>().color = new Color(0.990566f, 0.9625216f, 0.09812211f, 0.6901961f);
        yield return new WaitForSeconds(.5f);
        thisTarget.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.6901961f);
    }

}
