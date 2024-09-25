using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactableTile;
    //[SerializeField] private Tile wateredTile;

    [SerializeField] private GameObject cropGrowing;

    // Diccionario para rastrear si un tile ha sido plantado
    private Dictionary<Vector3Int, (bool isPlanted, int waterCount)> tileStates = new Dictionary<Vector3Int, (bool, int)>();

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

    public void SetInteracted(Vector3Int position) {
        if (!IsPlanted(position)) // Solo planta si no hay nada ya
        {
            interactableMap.SetTile(position, interactableTile);

            //Generar gameobject CropGrowing en la posición correspondiente
            Vector3 worldPosition = interactableMap.GetCellCenterWorld(position);
            Instantiate(cropGrowing, worldPosition, Quaternion.identity);

            // Marcar el tile como plantado
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


}
