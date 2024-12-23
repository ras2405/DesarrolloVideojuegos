using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemSpawnConfig
{
    public GameObject prefab;
    public Item itemData; // Referencia al ScriptableObject Item
    public int spawnWeight = 1; // probabilidad de que salga el item como respawn
}

public class ItemRespawnManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemSpawnPoint
    {
        public Vector2 position;
        public GameObject itemPrefab;
        public bool isOccupied;
    }

    [Header("Configuración")]
    public ItemSpawnConfig[] itemConfigs; // Array de configuraciones de items
    public int itemsPerDay = 7;
    [SerializeField] private float spawnRadius = 10f;

    [Header("Referencias")]
    public Transform spawnCenter;
    public GameObject onCollectEffectPrefab; // Prefab del efecto de recolección

    private List<ItemSpawnPoint> spawnPoints = new List<ItemSpawnPoint>();
    private CycleDayController cycleController;
    private int lastSpawnDay = 0;
    private bool isSpawning = false;
    private bool isDayChanging = false;

    public GameObject[] predefinedItems; // los items que no tienen que ser eliminados (ejemplo semillas)

    private void Start()
    {
        cycleController = FindObjectOfType<CycleDayController>();
        if (cycleController == null)
        {
            Debug.LogError("No se encontró CycleDayController en la escena.");
            return;
        }

        GenerateSpawnPoints();
        SpawnItems();
        lastSpawnDay = CycleDayController.currentDay;
    }

    private void Update()
    {
        if (CycleDayController.currentDay != lastSpawnDay && !isDayChanging && !isSpawning)
        {
            StartNewDay();
        }
    }

    private void StartNewDay()
    {
        isDayChanging = true;

        CleanupExistingItems();

        isSpawning = true;
        GenerateSpawnPoints();
        SpawnItems();
        isSpawning = false;

        lastSpawnDay = CycleDayController.currentDay;
        isDayChanging = false;
    }

    public void OnItemCollected(ItemSpawnPoint spawnPoint)
    {
        if (spawnPoint != null)
        {
            spawnPoint.isOccupied = false;
            spawnPoint.itemPrefab = null;
        }
    }

    private void GenerateSpawnPoints()
    {
        spawnPoints.Clear();

        int attempts = itemsPerDay * 50; // Para generacion de puntos validos

        for (int i = 0; i < attempts; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(0f, spawnRadius);

            Vector2 position = (Vector2)spawnCenter.position + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
                Mathf.Sin(angle * Mathf.Deg2Rad) * distance
            );

            if (IsValidSpawnPoint(position))
            {
                ItemSpawnPoint spawnPoint = new ItemSpawnPoint
                {
                    position = position,
                    isOccupied = false
                };
                spawnPoints.Add(spawnPoint);
            }
        }
    }

    private bool IsValidSpawnPoint(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 2f);
        foreach (var collider in colliders)
        {
            if (!collider.isTrigger)
                return false;
        }
        return true;
    }

    private void SpawnItems()
    {
        if (spawnPoints.Count == 0) return;

        List<ItemSpawnPoint> availablePoints = new List<ItemSpawnPoint>(spawnPoints);

        for (int i = 0; i < itemsPerDay; i++)
        {
            if (availablePoints.Count == 0) break;
            int randomIndex = Random.Range(0, availablePoints.Count);
            ItemSpawnPoint selectedPoint = availablePoints[randomIndex];
            availablePoints.RemoveAt(randomIndex);

            SpawnSingleItem(selectedPoint);
        }
    }

    private void SpawnSingleItem(ItemSpawnPoint selectedPoint)
    {
        if (itemConfigs == null || itemConfigs.Length == 0 || selectedPoint == null) return;
        // Calcular el peso total
        int totalWeight = 0;
        foreach (var config in itemConfigs)
        {
            if (config != null && config.prefab != null)
            {
                totalWeight += config.spawnWeight;
            }
        }

        if (totalWeight <= 0)
        {
            Debug.Log("No hay peso total para los ítems, no se generará ninguno.");
            return;
            //totalWeight = 8;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        ItemSpawnConfig selectedConfig = null;

        // Seleccionar ítem basado en pesos
        foreach (var config in itemConfigs)
        {
            if (config == null || config.prefab == null) continue;
            cumulativeWeight += config.spawnWeight;
            if (randomWeight < cumulativeWeight)
            {
                selectedConfig = config;
                break;
            }
        }

        if (selectedConfig?.prefab == null) return;

        // Crear el nuevo ítem
        Vector2 spawnPos = selectedPoint.position + (Vector2)Random.insideUnitCircle * 0.1f;
        GameObject newItem = Instantiate(selectedConfig.prefab, spawnPos, Quaternion.identity);
        newItem.SetActive(true);

        Farming farmingComponent = newItem.GetComponent<Farming>() ?? newItem.AddComponent<Farming>();
        farmingComponent.isRespawnable = true;
        SetupItem(newItem, farmingComponent, selectedConfig);

        selectedPoint.isOccupied = true;
        selectedPoint.itemPrefab = selectedConfig.prefab;
    }

    private void SetupItem(GameObject newItem, Farming farmingComponent, ItemSpawnConfig config)
    {
        BoxCollider2D itemCollider = newItem.GetComponent<BoxCollider2D>();
        // Asegurarnos que tiene un Collider2D
        if (itemCollider == null)
        {
           itemCollider = newItem.AddComponent<BoxCollider2D>();
        }
        itemCollider.isTrigger = true;
        itemCollider.size = new Vector2(0.3f, 0.3f);
        itemCollider.enabled = true;

        // Configurar el componente Farming
        farmingComponent.onCollectEffect = onCollectEffectPrefab;
        farmingComponent.item = config.itemData;
        farmingComponent.count = 1;

        if (!farmingComponent.enabled)
        {
            farmingComponent.enabled = true;
        }
    }

    private void CleanupExistingItems()
    {
        if (isSpawning) return;

        Farming[] existingItems = FindObjectsOfType<Farming>(true);

        foreach (Farming item in existingItems)
        {
            if (item == null || item.gameObject == null) continue;
            if (item.isRespawnable)
            {
                Destroy(item.gameObject);
            }
        }

        foreach (var point in spawnPoints)
        {
            point.isOccupied = false;
            point.itemPrefab = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnCenter != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spawnCenter.position, spawnRadius);
        }
    }
}