using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    public TileManager tileManager;
    //public float actualTime = 0.0f;
   // public TMP_Text counterText;
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
/*
    void update() {
        actualTime += Time.deltaTime;
        updateTimeText();
    }

    void updateTimeText() {
        counterText.text = Mathf.FloorToInt(actualTime).ToString();
    
    }*/
}
