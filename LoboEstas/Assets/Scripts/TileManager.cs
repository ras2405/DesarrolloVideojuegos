using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactableTile;

    [SerializeField] private GameObject cropGrowing;

    void Start()
    {
        Debug.Log("TileManager, posicion en interactableMap?");
        //Posibilidad de cambiar los tiles que vemos en dev y cuando jugamos
        foreach (var position in interactableMap.cellBounds.allPositionsWithin) { // Mide en unidades de celda del tilemap no en pixeles
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Tierra_Seca_interactable") //&& tile.name == "Interactable_Visible"
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
                Debug.Log("Posicion en interactableMap: " + position);
                Debug.Log("TileManager - Tile name: " + tile.name);
            }
        }
    }

    public bool IsInteractable(Vector3Int position) {
        TileBase tile = interactableMap.GetTile(position);
        Debug.Log("TileManager - Obtener tile de TileBase:" + interactableMap.GetTile(position));
        if (tile != null)
        {
            Debug.Log("TileManager - Tile name" + tile.name);
            if (tile.name == "Tierra_Seca")
            {
                Debug.Log("TileManager - Tile is interactable");
                return true;
            }
        }
        else {
            Debug.Log("TileManager - No se encontro tile en esta posicion: " + position);

        }
   
        Debug.Log("TileManager -2  is not interactable");
        return false;
    }

    public void SetInteracted(Vector3Int position) { 
        interactableMap.SetTile(position, interactableTile);

        //Generar gameobject CropGrowing en la posición correspondiente
        Vector3 worldPosition = interactableMap.GetCellCenterWorld(position);
        Instantiate(cropGrowing, worldPosition, Quaternion.identity);
    }
}
