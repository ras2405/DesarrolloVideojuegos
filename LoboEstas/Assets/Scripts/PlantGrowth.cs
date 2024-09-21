/*using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Asigna el Tilemap desde el Inspector
    [SerializeField] private string tileName = "Seeds"; // Nombre del tile que estamos buscando
    [SerializeField] private float growthTime = 5f; // Tiempo para crecer completamente
    [SerializeField] private Sprite[] growthStages; // Array de sprites para las etapas de crecimiento

    private float timer = 0f;

    void Update()
    {
        GrowPlant();
    }

    private void GrowPlant()
    {
        // Obtener la celda en la que está el objeto
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null && tile.name == tileName)
        {
            timer += Time.deltaTime;

            // Calcular el índice de crecimiento basado en el tiempo
            int stageIndex = Mathf.FloorToInt((timer / growthTime) * growthStages.Length);

            if (stageIndex < growthStages.Length)
            {
                Debug.Log("Etapa de crecimiento: " + stageIndex);

                tilemap.SetTile(cellPosition, ScriptableObject.CreateInstance<Tile>()); // Crear un nuevo tile
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = growthStages[stageIndex]; // Asigna el sprite correspondiente
                tilemap.SetTile(cellPosition, newTile);
            }
        }
    }
}*/

using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Asigna el Tilemap desde el Inspector
    [SerializeField] private string tileName = "Seeds"; // Nombre del tile que estamos buscando
    [SerializeField] private float growthTime = 5f; // Tiempo para crecer completamente
    [SerializeField] private Sprite[] growthStages; // Array de sprites para las etapas de crecimiento

    private float[] timers; // Para almacenar el tiempo de cada planta
    private Vector3Int[] seedPositions; // Para almacenar las posiciones de los tiles "seed"
    private int seedCount; // Contador de seeds

    public float actualTime = 0.0f;
    public TMP_Text counterText;

    void Start()
    {
        // Inicializar las posiciones y los temporizadores
        seedCount = 0;

        // Contar el número de seeds
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
         //   Debug.Log("Seed positions found: " + position.x + "," + position.y);
            TileBase tile = tilemap.GetTile(position);
            if (tile != null && tile.name == tileName)
            {
                seedCount++;
            }
        }

        // Imprimir la cantidad de tiles "seed" encontrados
     //   Debug.Log("Número de tiles 'seed' encontrados: " + seedCount);

        // Crear arrays con el tamaño correcto
        seedPositions = new Vector3Int[seedCount];
        timers = new float[seedCount];

        // Almacenar las posiciones de los tiles "seed"
        int index = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null && tile.name == tileName)
            {
                seedPositions[index] = position;
                index++;
            }
        }
    //    Debug.Log("Seed positions stored: " + string.Join(", ", seedPositions));
    }

    void Update()
    {
        actualTime += Time.deltaTime;
        updateTimeText();
        //GrowPlants();
    }

    void updateTimeText()
    {
        counterText.text = Mathf.FloorToInt(actualTime).ToString();

    }

    private void GrowPlants()
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            TileBase tile = tilemap.GetTile(seedPositions[i]);
            if (tile != null && tile.name == tileName)
            {
                timers[i] += Time.deltaTime;
              //  Debug.Log($"Growing seed at {seedPositions[i]}, timer: {timers[i]}");

                // Calcular el índice de crecimiento basado en el tiempo
                int stageIndex = Mathf.FloorToInt((timers[i] / growthTime) * growthStages.Length);

                // Asegúrate de que el índice no exceda el tamaño del array
                if (stageIndex < growthStages.Length)
                {
                    // Crear un nuevo tile y asignar el sprite correspondiente
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = growthStages[stageIndex];

                    // Cambiar el tile en el Tilemap
                    tilemap.SetTile(seedPositions[i], newTile); // Cambia el tile
                 //   Debug.Log($"Changed tile at {seedPositions[i]} to stage {stageIndex} with sprite {newTile.sprite.name}");
                }
                else
                {
                   // Debug.Log($"Growth completed for seed at {seedPositions[i]}");
                }
            }
            else
            {
               // Debug.Log($"No tile found at {seedPositions[i]} or tile is not a seed.");
            }
        }
    }
}