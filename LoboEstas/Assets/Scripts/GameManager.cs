/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    public ItemContainer inventoryContainer;
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

        //DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>(); 
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    public ItemContainer inventoryContainer;
    public TileManager tileManager;

    public SaveSystem saveSystem; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        tileManager = GetComponent<TileManager>();

        // Instancia del saveSystem
        saveSystem = gameObject.AddComponent<SaveSystem>();
    }
}