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
        if (CycleDayController.currentDay != lastSpawnDay)
        {
            SpawnItems();
            lastSpawnDay = CycleDayController.currentDay;
        }
    }

    private void GenerateSpawnPoints()
    {
        spawnPoints.Clear();

        for (int i = 0; i < itemsPerDay * 2; i++)
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        return colliders.Length == 0;
    }

    private void SpawnItems()
    {
        CleanupExistingItems();

        foreach (var point in spawnPoints)
        {
            point.isOccupied = false;
        }

        for (int i = 0; i < itemsPerDay; i++)
        {
            SpawnSingleItem();
        }
    }

    private void SpawnSingleItem()
    {
        List<ItemSpawnPoint> availablePoints = spawnPoints.FindAll(p => !p.isOccupied);

        if (availablePoints.Count == 0 || itemConfigs.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn disponibles o no hay items configurados.");
            return;
        }

        // Calcular el peso total
        int totalWeight = 0;
        foreach (var config in itemConfigs)
        {
            totalWeight += config.spawnWeight;  // Sumar todos los pesos
        }

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        ItemSpawnConfig selectedConfig = null;

        // Selección del ítem basado en los pesos
        foreach (var config in itemConfigs)
        {
            cumulativeWeight += config.spawnWeight;

            if (randomWeight < cumulativeWeight)
            {
                selectedConfig = config;
                break;
            }
        }

        // Si no se selecciona un ítem (esto debería ser raro), devolver
        if (selectedConfig == null)
        {
            Debug.LogWarning("No se pudo seleccionar un item.");
            return;
        }

        int randomIndex = Random.Range(0, availablePoints.Count);
        ItemSpawnPoint selectedPoint = availablePoints[randomIndex];

        // Seleccionar config de item aleatoria
        //ItemSpawnConfig selectedConfig = itemConfigs[Random.Range(0, itemConfigs.Length)];

        // Crear el item
        GameObject newItem = Instantiate(selectedConfig.prefab, selectedPoint.position, Quaternion.identity);
        newItem.SetActive(true);

        // Configurar el componente Farming
        Farming farmingComponent = newItem.GetComponent<Farming>();
        if (farmingComponent == null)
        {
            farmingComponent = newItem.AddComponent<Farming>();
        }

        // Configurar los componentes necesarios
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
        itemCollider.size = new Vector2(0.3f, 0.3f); // PROBAR
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
        Farming[] existingItems = FindObjectsOfType<Farming>();
        //foreach (Farming item in existingItems)
        //{
        //    Destroy(item.gameObject);
        //}
        foreach (Farming item in existingItems)
        {
            // Verificamos si el item es uno de los predefinidos
            bool isPredefined = false;
            foreach (var predefinedItem in predefinedItems)
            {
                if (item.gameObject.CompareTag(predefinedItem.tag))  // Compara si el tag es el mismo
                {
                    isPredefined = true;
                    break;
                }
            }

            // Si no es predefinido, lo destruimos
            if (!isPredefined)
            {
                Destroy(item.gameObject);
            }
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