using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] InventoryToolbar inventory;

    string savePath;
    saveData data;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        savePath = Application.persistentDataPath + "/save.dat";

        if (!File.Exists(savePath))
        {
            saveData newData = new saveData();
            newData.playerPosition = Vector3.zero;
            SaveGame(newData);
        }

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
        return null;
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
        PlayerPrefs.SetInt("CurrentDay", 1); //poner en 1 luego de probar las cosas
        PlayerPrefs.SetFloat("GameTimeInMinutes", 300f); 
        PlayerPrefs.Save();

        CycleDayController.currentDay = 1; //poner en 1 luego de probar las cosas
        CycleDayController.gameTimeInMinutes = 300f; // dejar en 300f luego de probar cosas

        if (inventory != null)
        {
            inventory.ResetInventory();
        }

        SceneManager.LoadSceneAsync("Main");
    }

    public void ContinueButton()
    {
        StartCoroutine(LoadGameSceneAndRestoreData(data));
    }

    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; 
        }

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