using System;
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
    [SerializeField] float characterWaitTime = 5f;

    Character characterToAdd;
    Item itemToAdd;
    List<Character> VisitedCharacters;


    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        animationSpawner = FindObjectOfType<AnimationSpawner>();
        SetButton1();
        VisitedCharacters = gameStatus.GetListVisitingCharacters();



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
                    //TODO replace instantiate with animations
                    //Character setCharacter = Instantiate(setGameItem.GetAttractedCharacter1(), transform.position, Quaternion.identity) as Character;
                    characterToAdd = setGameItem.GetAttractedCharacter();
                    itemToAdd = setGameItem;
                    SpawnAnimationForCharacterAndItem();
                    AddCharacterToVisitedCharacters();
                }
            }
        }
    }

    private void SpawnAnimationForCharacterAndItem()
    {
        AnimationScript animationToSpawn = animationSpawner.ReturnRequestedAnimation(characterToAdd, itemToAdd);
        AnimationScript setAnimation = Instantiate(animationToSpawn, setButtonLocation.position, Quaternion.identity) as AnimationScript;
    }

    private void AddCharacterToVisitedCharacters()
    {
        if (!VisitedCharacters.Contains(characterToAdd))
        {
            Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setGameItem.ItemName);
            characterToAdd.AddToItemsAttractedBy(setGameItem);
            characterToAdd.AddToVisits();
            gameStatus.AddToVisitedCharacters(characterToAdd);
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
                characterWaitTime = 5000f;
            }
            else if (ItemListToCheck.Contains(setGameItem.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setGameItem.ItemName);
                gameStatus.AddToCharacterVisitsOnly(characterToAdd);
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
            setButton.GetComponent<Image>().enabled = false;
            setButton.GetComponent<Button>().enabled = false;
            setButtonText.GetComponent<Text>().enabled = false;
        }

        if (gameStatus.getSetItem1() != null && !gameStatus.setModeActive)
        {
            setGameItem = Instantiate(gameStatus.getSetItem1(), setButtonLocation);
            setGameItem.transform.localScale += new Vector3(100f, 100f, 0);
            setGameItem.itemIsInGameScreen = true;
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
        SceneManager.LoadScene("PlayScreen");
    }

    private void HideSetArea()
    {
        setButton.GetComponent<Image>().enabled = false;
        setButton.GetComponent<Button>().enabled = false;
        setButtonText.GetComponent<Text>().enabled = false;
    }


}
