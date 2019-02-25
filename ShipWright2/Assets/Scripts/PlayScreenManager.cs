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
    List<Character> VisitedCharacters;
    Item itemToAdd;

    List<Item> SetItems;
    List<int> setItemNumber;
    [SerializeField] AudioClip cameraSound;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateGameObjects();
        PopulateSetButtons();
        toggleCamera();
        if (setGameItem1 != null || setGameItem2 != null || setGameItem3 != null)
        {
            StartCoroutine(CountDownAndSpawnACharacter());
        }
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
                setGameItem1.itemIsInGameScreen = true;
            }
            if (setItem2 != null)
            {
                setGameItem2 = Instantiate(setItem2, setButtonLocation2);
                setGameItem2.transform.localScale += new Vector3(100f, 100f, 0);
                setGameItem2.itemIsInGameScreen = true;
            }
            if (setItem3 != null)
            {
                setGameItem3 = Instantiate(setItem3, setButtonLocation3);
                setGameItem3.transform.localScale += new Vector3(100f, 100f, 0);
                setGameItem3.itemIsInGameScreen = true;
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

    private IEnumerator CountDownAndSpawnACharacter()
    {

        while (true)
        {
            SetItems = new List<Item>();
            setItemNumber = new List<int>();
            yield return new WaitForSeconds(gameStatus.characterWaitTime);


            if (setGameItem1 != null && gameStatus.setItem1.itemIsInGameScreen)
            {
                SetItems.Add(setGameItem1);
                setItemNumber.Add(1);
            }

            if (setGameItem2 != null && gameStatus.setItem2.itemIsInGameScreen)
            {
                SetItems.Add(setGameItem2);
                setItemNumber.Add(2);
            }
            if (setGameItem3 != null && gameStatus.setItem3.itemIsInGameScreen)
            {
                SetItems.Add(setGameItem3);
                setItemNumber.Add(3);
            }

            int randomSpawn = UnityEngine.Random.Range(0, SetItems.Count - 1);
            itemToAdd = SetItems[randomSpawn];
            if (setGameItem1 == itemToAdd && setGameItem1 != null)
            {
                gameStatus.setItem1.itemIsInGameScreen = false;
            }
            if (setGameItem2 == itemToAdd && setGameItem2 != null)
            {
                gameStatus.setItem2.itemIsInGameScreen = false;

            }
            if (setGameItem3 == itemToAdd && setGameItem3 != null)
            {
                gameStatus.setItem3.itemIsInGameScreen = false;

            }

            if (SetItems != null)
            {
                characterToAdd = SetItems[randomSpawn].GetAttractedCharacter();
                int setItemNumberToUse = setItemNumber[randomSpawn];


                SpawnAnimationForCharacterAndItem(characterToAdd, itemToAdd, setItemNumberToUse);
                //AddCharacterToVisitedCharacters();
            }
        }

    }


    private void SpawnAnimationForCharacterAndItem(Character characterToAdd, Item itemToAdd, int setItemNumberToUse)
    {
        string targetLocationString = "SetButton" + setItemNumberToUse.ToString();
        Transform targetLocation = GameObject.Find(targetLocationString).transform;
        AnimationScript animationToSpawn = animationSpawner.ReturnRequestedAnimation(characterToAdd, itemToAdd);
        AnimationScript setAnimation = Instantiate(animationToSpawn, targetLocation.position, Quaternion.identity) as AnimationScript;
        gameStatus.pictureModeActive = true;
        toggleCamera();
        StartCoroutine(WaitThenRemoveAnimation());
    }

    private IEnumerator WaitThenRemoveAnimation()
    {
        yield return new WaitForSeconds(gameStatus.GetTimeBeforeRemove());
        Destroy(gameObject);
        itemToAdd.itemIsInGameScreen = true;
        gameStatus.pictureModeActive = false;
        PopulateSetButtons();
    }

    private void AddCharacterToVisitedCharacters()
    {
        if (!VisitedCharacters.Contains(characterToAdd))
        {
            Debug.Log(characterToAdd.characterName + " has Appeared. Drawn by " + setGameItem1.ItemName);
            characterToAdd.AddToItemsAttractedBy(setGameItem1);
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
            if (!ItemListToCheck.Contains(setGameItem1.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by New Item" + setGameItem1.ItemName);
                gameStatus.AddItemToVisitedCharacter(characterToAdd, setGameItem1);
                gameStatus.Save();
                characterWaitTime = 5000f;
            }
            else if (ItemListToCheck.Contains(setGameItem1.ItemName))
            {
                Debug.Log(characterToAdd.characterName + " has Appeared Again. Drawn by Old Item" + setGameItem1.ItemName);
                gameStatus.AddToCharacterVisitsOnly(characterToAdd);
                gameStatus.Save();
                characterWaitTime = 5000f;
            }
        }
    }


    private void toggleCamera()
    {
        GameObject pictureTaker = GameObject.Find("PictureTaker");
        if (!gameStatus.pictureModeActive)
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

    public void TakeAPicture2()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem2.ItemName;
        string characterForPicture = characterToAdd.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target2));

    }


    public void TakeAPicture3()
    {
        Debug.Log("Picture Taken");
        string itemForPicture = gameStatus.setItem3.ItemName;
        string characterForPicture = characterToAdd.characterName;
        gameStatus.AddAPicture(characterForPicture, itemForPicture);
        StartCoroutine(Flash(target2));

    }

    private IEnumerator Flash(GameObject target)
    {
        GameObject thisTarget = target;
        AudioSource.PlayClipAtPoint(cameraSound, Camera.main.transform.position);
        thisTarget.GetComponent<Image>().color = new Color(0.990566f, 0.9625216f, 0.09812211f, 0.6901961f);
        yield return new WaitForSeconds(.5f);
        thisTarget.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.6901961f);
    }


    private void InstantiateGameObjects()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        animationSpawner = FindObjectOfType<AnimationSpawner>();
        VisitedCharacters = gameStatus.GetListVisitingCharacters();
        characterWaitTime = gameStatus.GetTimeBeforeSpawn();
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
