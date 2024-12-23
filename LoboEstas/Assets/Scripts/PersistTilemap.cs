using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistTilemap : MonoBehaviour
{
    private static PersistTilemap instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
