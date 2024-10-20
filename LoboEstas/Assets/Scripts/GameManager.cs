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
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    public ItemContainer inventoryContainer;
    public TileManager tileManager;

    // Agregar referencia al saveSystem
    public saveSystem saveSystem; // Asegúrate de que la clase saveSystem esté definida

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Opcional: si quieres que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        tileManager = GetComponent<TileManager>();

        // Crear una instancia del saveSystem
        saveSystem = gameObject.AddComponent<saveSystem>(); // Asegúrate de que saveSystem es un MonoBehaviour
    }
}