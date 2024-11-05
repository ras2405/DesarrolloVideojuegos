using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Queremos guardar la posici�n del Player
    PlayerController playerController;

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
        // Borrar el archivo de guardado
        /*if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        // Crear una nueva partida
        saveData newData = new saveData();
        newData.playerPosition = Vector3.zero; // Reiniciar la posici�n
        print("NewGameButton playerPos: " + newData.playerPosition);
        SaveGame(newData);

        // Iniciar el juego en la escena principal
        StartCoroutine(LoadGameSceneAndRestoreData(newData));*/
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CurrentDay", 1); // D�a 1
        PlayerPrefs.SetFloat("GameTimeInMinutes", 300f);
        PlayerPrefs.Save();

        //TODO: Limpiar inventario

        SceneManager.LoadSceneAsync("Main");
    }

    public void ContinueButton()
    {
        // Cargar la escena principal y restaurar la posici�n del jugador
        StartCoroutine(LoadGameSceneAndRestoreData(data));
    }

    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera as�ncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController est� disponible
        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; 
        }

        // Restaurar la posici�n del jugador
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