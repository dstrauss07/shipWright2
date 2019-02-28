using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayScreenManager : MonoBehaviour
{

    GameStatus gameStatus;
    AnimationSpawner animationSpawner;

    GameObject setButton1;
    Transform setButtonLocation1;
    GameObject setButtonText1;
    GameObject target1;
    Item setGameItem1;

    GameObject setButton2;
    Transform setButtonLocation2;
    GameObject setButtonText2;
    GameObject target2;
    Item setGameItem2;


    GameObject setButton3;
    Transform setButtonLocation3;
    GameObject setButtonText3;
    GameObject target3;
    Item setGameItem3;

    float characterWaitTime;
    Character characterToAdd;
    Character characterToAdd1;
    Character characterToAdd2;
    Character characterToAdd3;
    List<Character> VisitedCharacters;
    Item itemToAdd;

    List<Item> SetItems;
    List<int> setItemNumber;
    [SerializeField] AudioClip cameraSound;

    [SerializeField] List<target> targetList;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGameObjects();
        PopulateSetButtons();
        toggleCamera();
        StartCoroutine(CountDownAndSpawnACharacter1());
        StartCoroutine(CountDownAndSpawnACharacter2());
        StartCoroutine(CountDownAndSpawnACharacter3());
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void PopulateSetButtons()
    {
        Item setItem1 = gameStatus.setItem1;
        Item setItem2 = gameStatus.setItem2;
        Item setItem3 = gameStatus.setItem3;

        if (!gameStatus.setModeActive)
        {
            if (setItem1 != null)
            {
                setGameItem1 = Instantiate(setItem1, setButtonLocation1);
                setGameItem1.transform.localScale += new Vector3(100f, 100f, 0);
                gameStatus.setItem1.itemIsInGameScreen = true;
            }
            if (setItem2 != null)
            {
                setGameItem2 = Instantiate(setItem2, setButtonLocation2);
                setGameItem2.transform.localScale += new Vector3(100f, 100f, 0);
                gameStatus.setItem2.itemIsInGameScreen = true;
            }
            if (setItem3 != null)
            {
                setGameItem3 = Instantiate(setItem3, setButtonLocation3);
                setGameItem3.transform.localScale += new Vector3(100f, 100f, 0);
                gameStatus.setItem3.itemIsInGameScreen = true;
            }

            HideSetArea();
                   }
        if (gameStatus.setModeActive)
        {
            if (setItem1 != null)
            {
                setGameItem1 = Instantiate(setItem1, setButtonLocation1);
                setGameItem1.transform.localScale += new Vector3(75f, 75f, 0);
                Color currentImage = setGameItem1.GetComponent<SpriteRenderer>().color;
                currentImage.a = 0.25f;
                setGameItem1.GetComponent<SpriteRenderer>().color = currentImage;
                setGameItem1.itemIsInGameScreen = false;
            }
            if (setItem2 != null)
            {
                setGameItem2 = Instantiate(setItem2, setButtonLocation2);
                setGameItem2.transform.localScale += new Vector3(75f, 75f, 0);
                Color currentImage = setGameItem2.GetComponent<SpriteRenderer>().color;
                currentImage.a = 0.25f;
                setGameItem2.GetComponent<SpriteRenderer>().color = currentImage;
                setGameItem2.itemIsInGameScreen = false;
            }
            if (setItem3 != null)
            {
                setGameItem3 = Instantiate(setItem3, setButtonLocation3);
                setGameItem3.transform.localScale += new Vector3(75f, 75f, 0);
                Color currentImage = setGameItem3.GetComponent<SpriteRenderer>().color;
                currentImage.a = 0.25f;
                setGameItem3.GetComponent<SpriteRenderer>().color = currentImage;
                setGameItem3.itemIsInGameScreen = false;
            }

        }
    }

    public void setItem1Here()
    {
        setGameItem1 = Instantiate(gameStatus.itemToSet, setButtonLocation1);
        setGameItem1.transform.localScale += new Vector3(100f, 100f, 0);
        gameStatus.setModeActive = false;
        gameStatus.SetItem1(gameStatus.itemToSet);
        gameStatus.Save();
        SceneManager.LoadScene("PlayScreen");
    }

    public void setItem2Here()
    {
        setGameItem2 = Instantiate(gameStatus.itemToSet, setButtonLocation2);
        setGameItem2.transform.localScale += new Vector3(100f, 100f, 0);
        gameStatus.setModeActive = false;
        gameStatus.SetItem2(gameStatus.itemToSet);
        gameStatus.Save();
        SceneManager.LoadScene("PlayScreen");
    }

    public void setItem3Here()
    {
        setGameItem3 = Instantiate(gameStatus.itemToSet, setButtonLocation3);
        setGameItem3.transform.localScale += new Vector3(100f, 100f, 0);
        gameStatus.setModeActive = false;
        gameStatus.SetItem3(gameStatus.itemToSet);
        gameStatus.Save();
        SceneManager.LoadScene("PlayScreen");
    }

    private void HideSetArea()
    {
        setButton1.GetComponent<Image>().enabled = false;
        setButton1.GetComponent<Button>().enabled = false;
        setButtonText1.GetComponent<Text>().enabled = false;
        setButton2.GetComponent<Image>().enabled = false;
        setButton2.GetComponent<Button>().enabled = false;
        setButtonText2.GetComponent<Text>().enabled = false;
        setButton3.GetComponent<Image>().enabled = false;
        setButton3.GetComponent<Button>().enabled = false;
        setButtonText3.GetComponent<Text>().enabled = false;
    }

    private IEnumerator CountDownAndSpawnACharacter1()
    {
         while (gameStatus.setItem1 != null && gameStatus.setItem1.itemIsInGameScreen)
        {       yield return new WaitForSeconds(UnityEngine.Random.Range(gameStatus.characterWaitMinTime, gameStatus.characterWaitMaxTime));
                gameStatus.setItem1.itemIsInGameScreen = false;
                characterToAdd1 = gameStatus.setItem1.GetAttractedCharacter();
                SpawnAnimationForCharacterAndItem(characterToAdd1, gameStatus.setItem1, 1);
                AddCharacterToVisitedCharacters(characterToAdd1, gameStatus.setItem1);
          }

    }
    private IEnumerator CountDownAndSpawnACharacter2()
    {
        while (gameStatus.setItem1 != null && gameStatus.setItem1.itemIsInGameScreen)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(gameStatus.characterWaitMinTime, gameStatus.characterWaitMaxTime));
            gameStatus.setItem1.itemIsInGameScreen = false;
            characterToAdd2 = gameStatus.setItem2.GetAttractedCharacter();
            SpawnAnimationForCharacterAndItem(characterToAdd2, gameStatus.setItem2, 2);
            AddCharacterToVisitedCharacters(characterToAdd2, gameStatus.setItem2);
        }

    }
    private IEnumerator CountDownAndSpawnACharacter3()
    {
        while (gameStatus.setItem3 != null && gameStatus.setItem3.itemIsInGameScreen)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(gameStatus.characterWaitMinTime, gameStatus.characterWaitMaxTime));
            gameStatus.setItem3.itemIsInGameScreen = false;
            characterToAdd3 = gameStatus.setItem3.GetAttractedCharacter();
            SpawnAnimationForCharacterAndItem(characterToAdd3, gameStatus.setItem3, 3);
            AddCharacterToVisitedCharacters(characterToAdd3, gameStatus.setItem3);
        }

    }


    private void SpawnAnimationForCharacterAndItem(Character characterToAdd, Item itemToAdd, int setItemNumberToUse)
    {
        string targetLocationString = "SetButton" + setItemNumberToUse.ToString();
        Transform targetLocation = GameObject.Find(targetLocationString).transform;
       // string targetToActivateString = "Target" + setItemNumberToUse.ToString();
        target targetToActivate = targetList[setItemNumberToUse - 1];
        targetToActivate.hasCharacter = true;
        toggleCamera();
        AnimationScript animationToSpawn = animationSpawner.ReturnRequestedAnimation(characterToAdd, itemToAdd);
        AnimationScript setAnimation = Instantiate(animationToSpawn, targetLocation.position, Quaternion.identity) as AnimationScript;
        StartCoroutine(WaitThenRemoveAnimation(setAnimation, itemToAdd));
    }

    private IEnumerator WaitThenRemoveAnimation(AnimationScript setAnimation, Item itemToAdd)
    {
        yield return new WaitForSeconds(gameStatus.GetTimeBeforeRemove());

        Destroy(setAnimation.gameObject);
        if (gameStatus.setItem1 == itemToAdd && gameStatus.setItem1 != null)
        {
            gameStatus.setItem1.itemIsInGameScreen = true;
            target1.GetComponent<Image>().enabled = false;
            target1.GetComponent<Button>().enabled = false;
            StartCoroutine(CountDownAndSpawnACharacter1());
        }
        if (gameStatus.setItem2 == itemToAdd && gameStatus.setItem2 != null)
        {
            gameStatus.setItem2.itemIsInGameScreen = true;
            target2.GetComponent<Image>().enabled = false;
            target2.GetComponent<Button>().enabled = false;
            StartCoroutine(CountDownAndSpawnACharacter2());
        }
        if (gameStatus.setItem3 == itemToAdd && gameStatus.setItem3 != null)
        {
            gameStatus.setItem3.itemIsInGameScreen = true;
            target3.GetComponent<Image>().enabled = false;
            target3.GetComponent<Button>().enabled = false;
            StartCoroutine(CountDownAndSpawnACharacter3());
        }
    }


    private void AddCharacterToVisitedCharacters(Character characterToAdd, Item itemToAdd)
    {
        if (!VisitedCharacters.Contains(characterToAdd))
        {
            Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + itemToAdd.ItemName);
            characterToAdd.AddToItemsAttractedBy(itemToAdd);
            characterToAdd.AddToVisits();
            characterToAdd.lastVisitDateTimeString = DateTime.Now.ToString();
            gameStatus.AddToVisitedCharacters(characterToAdd);
            gameStatus.Save();
          }
        else if (VisitedCharacters.Contains(characterToAdd))
        {
            var CharacterToUpdate = VisitedCharacters.Find(c => c.characterName == characterToAdd.characterName);
            var ItemListToCheck = CharacterToUpdate.ReturnItemNames();
            if (!ItemListToCheck.Contains(itemToAdd.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + itemToAdd.ItemName);
                gameStatus.AddItemToVisitedCharacter(characterToAdd, itemToAdd);
                gameStatus.Save();
             }
            else if (ItemListToCheck.Contains(itemToAdd.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + itemToAdd.ItemName);
                gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                gameStatus.Save();
             }
        }
    }


    private void toggleCamera()
    {
        GameObject pictureTaker = GameObject.Find("PictureTaker");
        if (targetList[0].hasCharacter || targetList[1].hasCharacter || targetList[2].hasCharacter)
        {
            pictureTaker.GetComponent<Image>().enabled = true;
            pictureTaker.GetComponent<Button>().enabled = true;
        }
        else
        {
            pictureTaker.GetComponent<Image>().enabled = false;
            pictureTaker.GetComponent<Button>().enabled = false;
        }
    }

    public void turnOnTargets()
    {
        Debug.Log("take a picture");
        foreach (target target in targetList)
        {
            if (target.hasCharacter)
            {
                target.GetComponent<Button>().enabled = true;
                target.GetComponent<Image>().enabled = true;
            }
        }
    }


    public void turnOffTargets()
    {
        Debug.Log("take a picture");
        foreach (target target in targetList)
        {
    
                target.GetComponent<Button>().enabled = false;
                target.GetComponent<Image>().enabled = false;

        }
    }

    public void TakeAPicture1()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem1.ItemName;
        string characterForPicture = characterToAdd1.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target1));

    }

    public void TakeAPicture2()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem2.ItemName;
        string characterForPicture = characterToAdd2.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target2));

    }


    public void TakeAPicture3()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem3.ItemName;
        string characterForPicture = characterToAdd3.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target3));

    }

    private IEnumerator Flash(GameObject target)
    {
        GameObject thisTarget = target;
        AudioSource.PlayClipAtPoint(cameraSound, Camera.main.transform.position);
        thisTarget.GetComponent<Image>().color = new Color(0.990566f, 0.9625216f, 0.09812211f, 0.6901961f);
        yield return new WaitForSeconds(.5f);
        thisTarget.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.6901961f);
        turnOffTargets();
    }


    private void InstantiateGameObjects()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        animationSpawner = FindObjectOfType<AnimationSpawner>();
        VisitedCharacters = gameStatus.GetListVisitingCharacters();
        target1 = GameObject.Find("Target1");
        target1.GetComponent<Image>().enabled = false;
        target1.GetComponent<Button>().enabled = false;
        target2 = GameObject.Find("Target2");
        target2.GetComponent<Image>().enabled = false;
        target2.GetComponent<Button>().enabled = false;
        target3 = GameObject.Find("Target3");
        target3.GetComponent<Image>().enabled = false;
        target3.GetComponent<Button>().enabled = false;
        setButton1 = GameObject.Find("SetButton1");
        setButtonText1 = GameObject.Find("SetButtonText1");
        setButtonLocation1 = setButton1.transform;
        setButton2 = GameObject.Find("SetButton2");
        setButtonText2 = GameObject.Find("SetButtonText2");
        setButtonLocation2 = setButton2.transform;
        setButton3 = GameObject.Find("SetButton3");
        setButtonText3 = GameObject.Find("SetButtonText3");
        setButtonLocation3 = setButton3.transform;
    }

}
