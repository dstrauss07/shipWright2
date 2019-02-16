using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpawner : MonoBehaviour
{
    [SerializeField] List<AnimationScript> animationsToSpawn;

    public AnimationScript ReturnRequestedAnimation(Character characterToAdd, Item itemToSet)
    {

        string characterItemString = characterToAdd.characterName + itemToSet.ItemName;
        Debug.Log("Searching for " + characterItemString);
        AnimationScript animationToReturn = animationsToSpawn.Find(a => a.AnimationName == characterItemString);
        Debug.Log("spawning " + animationToReturn.AnimationName);
        return animationToReturn;
    }


}
