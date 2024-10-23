using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistTilemap : MonoBehaviour
{
    private static PersistTilemap instance;

    private void Awake()
    {
        // Si ya existe una instancia de este objeto, destruir el duplicado
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hacer que el Tilemap persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados si existen
        }
    }
}
