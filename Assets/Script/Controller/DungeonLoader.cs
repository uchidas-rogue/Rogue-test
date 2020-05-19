using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLoader : MonoBehaviour
{
    //tha gameobject GameManager is attached to 
    public GameObject gameManager;

    void Awake() 
    {
        if (GameManager.Singleton == null)
        {
            Instantiate(gameManager);
        }   
    }
}
