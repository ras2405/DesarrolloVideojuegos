using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    public TileManager tileManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>(); 
    }
}
