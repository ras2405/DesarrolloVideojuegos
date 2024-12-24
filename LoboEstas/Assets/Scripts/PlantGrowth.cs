using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; 
    [SerializeField] private string tileName = "Seeds"; 
    [SerializeField] private float growthTime = 5f; 
    [SerializeField] private Sprite[] growthStages; 

    private float[] timers; 
    private Vector3Int[] seedPositions; 
    private int seedCount; 

    public float actualTime = 0.0f;
    public TMP_Text counterText;

    void Start()
    {
        seedCount = 0;

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null && tile.name == tileName)
            {
                seedCount++;
            }
        }

        seedPositions = new Vector3Int[seedCount];
        timers = new float[seedCount];

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
    }

    void Update()
    {
        actualTime += Time.deltaTime;
        updateTimeText();
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

                int stageIndex = Mathf.FloorToInt((timers[i] / growthTime) * growthStages.Length);

                if (stageIndex < growthStages.Length)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = growthStages[stageIndex];

                    tilemap.SetTile(seedPositions[i], newTile); 
                }
            }
        }
    }
}