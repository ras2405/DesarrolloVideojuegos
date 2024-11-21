using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Queremos guardar la posición del Player
    PlayerController playerController;
    [SerializeField] InventoryToolbar inventory;

    string savePath;
    saveData data;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        savePath = Application.persistentDataPath + "/save.dat";

        // Verificar si existe un archivo de guardado
        if (!File.Exists(savePath))
        {
            // Crear un nuevo archivo de guardado si no existe
            saveData newData = new saveData();
            newData.playerPosition = Vector3.zero;
            SaveGame(newData);
        }

        // Cargar los datos guardados
        data = LoadGame();
    }

    void SaveGame(saveData dataToSave)
    {
        string JsonData = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(savePath, JsonData);
    }

    public saveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string loadedData = File.ReadAllText(savePath);
            saveData dataToReturn = JsonUtility.FromJson<saveData>(loadedData);
            return dataToReturn;
        }
        return null; // Retornar null si no hay datos
    }

    public void SaveGameButton()
    {
        Vector3 playerPos = playerController.GetPosition();
        print("SaveGameButton playerPos: " + playerPos);
        data.playerPosition = playerPos;
        SaveGame(data);
    }

    public void NewGameButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CurrentDay", 1); // Día 1
        PlayerPrefs.SetFloat("GameTimeInMinutes", 300f);
        PlayerPrefs.Save();

        CycleDayController.currentDay = 1;
        CycleDayController.gameTimeInMinutes = 300f;

        //TODO: Limpiar inventario
        //InventoryToolbar inventoryToolbar = FindObjectOfType<InventoryToolbar>(); // Buscar el script de inventario en la escena
        if (inventory != null)
        {
            Debug.Log("HOLA");
            inventory.ResetInventory(); // Llamar al método que limpia el inventario
        }

        SceneManager.LoadSceneAsync("Main");
    }

    public void ContinueButton()
    {
        // Cargar la escena principal y restaurar la posición del jugador
        StartCoroutine(LoadGameSceneAndRestoreData(data));
    }

    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController esté disponible
        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; 
        }

        // Restaurar la posición del jugador
        print("ContinueButton playerPos: " + dataToRestore.playerPosition);
        playerController.SetPosition(dataToRestore.playerPosition);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class saveData
{
    public Vector3 playerPosition;
}