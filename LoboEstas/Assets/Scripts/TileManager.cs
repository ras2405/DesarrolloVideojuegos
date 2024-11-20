/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactableTile;
    //[SerializeField] private Tile wateredTile;

    //[SerializeField] private GameObject cropGrowing;
    [SerializeField] private GameObject cropGrowingPrefab;
    [SerializeField] private GameObject potatoGrowingPrefab;
    [SerializeField] private GameObject onionGrowingPrefab;

    // Diccionario para rastrear si un tile ha sido plantado
    private Dictionary<Vector3Int, Crop> cropsOnTiles = new Dictionary<Vector3Int, Crop>();
    private Dictionary<Vector3Int, (bool isPlanted, int waterCount)> tileStates = new Dictionary<Vector3Int, (bool, int)>();

    private void Awake()
    {
        DontDestroyOnLoad(cropGrowingPrefab); // Esto asegura que este GameObject no se destruya al cambiar de escena.
    }

    void Start()
    {
        //Posibilidad de cambiar los tiles que vemos en dev y cuando jugamos
        foreach (var position in interactableMap.cellBounds.allPositionsWithin) { // Mide en unidades de celda del tilemap no en pixeles
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Tierra_Seca_interactable") 
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
                tileStates[position] = (false, 0); // Inicializa como no plantado y sin agua 
                // Inicializa como no plantado, luego de cultivar queremos que vuelva a estar vacio y saber que esta plantado y cuantas veces se rego
            }
        }
    }

    public bool IsInteractable(Vector3Int position) {
        TileBase tile = interactableMap.GetTile(position);
        //Debug.Log("TileManager - Obtener tile de TileBase:" + interactableMap.GetTile(position));
        if (tile != null)
        {
            // Debug.Log("TileManager - Tile name" + tile.name);
            if (tile.name == "Tierra_Seca")
            {
                //Debug.Log("TileManager - Tile is interactable");
                return true;
            }
        }
        else {
           //Debug.Log("TileManager - No se encontro tile en esta posicion: " + position);
        }
        return false;
    }


    public void SetInteracted(Vector3Int position, string seed) //NEW
    {
        if (!IsPlanted(position))
        {
            interactableMap.SetTile(position, interactableTile);

            Vector3 worldPosition = interactableMap.GetCellCenterWorld(position);
            GameObject cropObject = null;

            if (seed == "OnionSeed")
                cropObject = Instantiate(onionGrowingPrefab, worldPosition, Quaternion.identity);
            else if (seed == "PotatoSeed")
                cropObject = Instantiate(potatoGrowingPrefab, worldPosition, Quaternion.identity);
            else if (seed == "CarrotSeed")
            {
                cropObject = Instantiate(cropGrowingPrefab, worldPosition, Quaternion.identity);
                //Debug.Log("Se crea un cropGrowingPrefab ");
            }

            // Registrar el crop en el diccionario
            if (cropObject != null)
            {
                Crop crop = cropObject.GetComponent<Crop>();
                cropsOnTiles[position] = crop;
            }

            tileStates[position] = (true, 0);
            //Debug.Log("Planta sembrada en: " + position);
        }
        else
        {
            //Debug.Log("Ya hay algo plantado en: " + position);
        }
    }

    public void WaterPlant(Vector3Int position)
    {
        if (IsPlanted(position))
        {
            var (isPlanted, waterCount) = tileStates[position];
            waterCount++;
            tileStates[position] = (isPlanted, waterCount);
            cropsOnTiles[position].WaterPlant();
            // Debug.Log($"Planta regada en {position}. Total de riegos: {waterCount}");
        }
        else
        {
            // Debug.Log("No hay planta en este tile.");
        }
    }

    public bool IsPlanted(Vector3Int position)
    {
        return tileStates.TryGetValue(position, out var state) && state.isPlanted;
    }

    public void ResetTile(Vector3Int position)
    {
        if (IsPlanted(position))
        {
            // Restablece el tile a "Tierra_Seca_interactable" (o cualquier tile que uses para el suelo vac�o)
            interactableMap.SetTile(position, hiddenInteractableTile);

            // Restablece el estado del tile en el diccionario
            tileStates[position] = (false, 0);

            Debug.Log($"El tile en {position} est� ahora listo para ser plantado de nuevo.");
        }
    }

}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactableTile;
    //[SerializeField] private Tile wateredTile;

    //[SerializeField] private GameObject cropGrowing;
    [SerializeField] private GameObject carrotGrowingPrefab;
    [SerializeField] private GameObject potatoGrowingPrefab;
    [SerializeField] private GameObject onionGrowingPrefab;

    // Diccionario para rastrear si un tile ha sido plantado
    private Dictionary<Vector3Int, Crop> cropsOnTiles = new Dictionary<Vector3Int, Crop>(); //NEW
    private Dictionary<Vector3Int, (bool isPlanted, int waterCount)> tileStates = new Dictionary<Vector3Int, (bool, int)>();

    private void Awake()
    {
        DontDestroyOnLoad(carrotGrowingPrefab);
        DontDestroyOnLoad(potatoGrowingPrefab);
        DontDestroyOnLoad(onionGrowingPrefab);
    }

    void Start()
    {
        //Posibilidad de cambiar los tiles que vemos en dev y cuando jugamos
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        { // Mide en unidades de celda del tilemap no en pixeles
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Tierra_Seca_interactable")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
                tileStates[position] = (false, 0); // Inicializa como no plantado y sin agua 
                // Inicializa como no plantado, luego de cultivar queremos que vuelva a estar vacio y saber que esta plantado y cuantas veces se rego
            }
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        //Debug.Log("TileManager - Obtener tile de TileBase:" + interactableMap.GetTile(position));
        if (tile != null)
        {
            // Debug.Log("TileManager - Tile name" + tile.name);
            if (tile.name == "Tierra_Seca")
            {
                //Debug.Log("TileManager - Tile is interactable");
                return true;
            }
        }
        else
        {
            //Debug.Log("TileManager - No se encontro tile en esta posicion: " + position);
        }
        return false;
    }

    public void SetInteracted(Vector3Int position, string seed) //NEW
    {
        Debug.Log("SetInteracted con seed: " + seed);
        if (!IsPlanted(position))
        {
            interactableMap.SetTile(position, interactableTile);

            Vector3 worldPosition = interactableMap.GetCellCenterWorld(position);
            GameObject cropObject = null;

            if (seed == "OnionSeed") {
                Debug.Log("Se crea un onionGrowingPrefab ");
                cropObject = Instantiate(onionGrowingPrefab, worldPosition, Quaternion.identity);
            }
            else if (seed == "Potato Seed") {
                Debug.Log("Se crea un potatoGrowingPrefab ");
                cropObject = Instantiate(potatoGrowingPrefab, worldPosition, Quaternion.identity);
            }
            else if (seed == "CarrotSeed")
            {
                Debug.Log("Se crea un carrotGrowingPrefab ");
                cropObject = Instantiate(carrotGrowingPrefab, worldPosition, Quaternion.identity);
            }

            // Registrar el crop en el diccionario
            if (cropObject != null)
            {
                Crop crop = cropObject.GetComponent<Crop>();
                cropsOnTiles[position] = crop;
            }

            tileStates[position] = (true, 0);
            Debug.Log("Planta sembrada en: " + position);
        }
        else
        {
            Debug.Log("Ya hay algo plantado en: " + position);
        }
    }


    public void WaterPlant(Vector3Int position)
    {
        if (IsPlanted(position))
        {
            var (isPlanted, waterCount) = tileStates[position];
            waterCount++;
            tileStates[position] = (isPlanted, waterCount);
            cropsOnTiles[position].WaterPlant();
            // Debug.Log($"Planta regada en {position}. Total de riegos: {waterCount}");
        }
        else
        {
            // Debug.Log("No hay planta en este tile.");
        }
    }

    public bool IsPlanted(Vector3Int position)
    {
        return tileStates.TryGetValue(position, out var state) && state.isPlanted;
    }

    public void ResetTile(Vector3Int position)
    {
        if (IsPlanted(position))
        {
            // Restablece el tile a "Tierra_Seca_interactable" (o cualquier tile que uses para el suelo vac�o)
            interactableMap.SetTile(position, hiddenInteractableTile);

            // Restablece el estado del tile en el diccionario
            tileStates[position] = (false, 0);

            //Debug.Log($"El tile en {position} esta ahora listo para ser plantado de nuevo.");
        }
    }

}
