using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{   
    // Config Parameters
    [SerializeField] int brakeableBlocks; //Serialized for debugging purposes.

    //cached refference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void AddBreakableBlock()
    {
        brakeableBlocks++;
    }

    public void RemoveBreakableObject()
    {
        brakeableBlocks--;
        if (brakeableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
    

}
