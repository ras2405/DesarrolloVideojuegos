using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile wateredInteractableTile;
    [SerializeField] private Tile interactableTile;

    [SerializeField] private GameObject carrotGrowingPrefab;
    [SerializeField] private GameObject potatoGrowingPrefab;
    [SerializeField] private GameObject onionGrowingPrefab;

    [SerializeField] private float wateredTileDuration = 5f;

    private Dictionary<Vector3Int, Crop> cropsOnTiles = new Dictionary<Vector3Int, Crop>(); 
    private Dictionary<Vector3Int, (bool isPlanted, int waterCount)> tileStates = new Dictionary<Vector3Int, (bool, int)>();

    private void Awake()
    {
        DontDestroyOnLoad(carrotGrowingPrefab);
        DontDestroyOnLoad(potatoGrowingPrefab);
        DontDestroyOnLoad(onionGrowingPrefab);
    }

    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        { 
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Tierra_Seca_interactable")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
                tileStates[position] = (false, 0); 
            }
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if (tile != null)
        {
            if (tile.name == "Tierra_Seca")
            {
                return true;
            }
        }
        return false;
    }

    public void SetInteracted(Vector3Int position, string seed)
    {
        Debug.Log("SetInteracted con seed: " + seed);
        if (!IsPlanted(position))
        {
            interactableMap.SetTile(position, interactableTile);

            Vector3 worldPosition = interactableMap.GetCellCenterWorld(position);
            GameObject cropObject = null;

            if (seed == "OnionSeed")
            {
                Debug.Log("Se crea un onionGrowingPrefab ");
                cropObject = Instantiate(onionGrowingPrefab, worldPosition, Quaternion.identity);
            }
            else if (seed == "Potato Seed")
            {
                Debug.Log("Se crea un potatoGrowingPrefab ");
                cropObject = Instantiate(potatoGrowingPrefab, worldPosition, Quaternion.identity);
            }
            else if (seed == "CarrotSeed")
            {
                Debug.Log("Se crea un carrotGrowingPrefab ");
                cropObject = Instantiate(carrotGrowingPrefab, worldPosition, Quaternion.identity);
            }

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

            StartCoroutine(TemporarilyChangeToWateredTile(position, 5f)); 

            Debug.Log($"Planta regada en {position}. Total de riegos: {waterCount}");
        }
        else
        {
            Debug.Log("No hay planta en este tile.");
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
            interactableMap.SetTile(position, hiddenInteractableTile);

            tileStates[position] = (false, 0);

        }
    }

    private IEnumerator TemporarilyChangeToWateredTile(Vector3Int position, float duration)
    {
        // Cambiar a tierra húmeda
        interactableMap.SetTile(position, wateredInteractableTile);

        yield return new WaitForSeconds(duration);

        // Restaurar a tierra seca
        interactableMap.SetTile(position, hiddenInteractableTile);
    }
}
