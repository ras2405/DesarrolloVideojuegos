/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class saveSystem : MonoBehaviour
{
    // Queremos guardar la posicion del Player
    PlayerController playerController;

    string savePath;
    saveData data;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        savePath = Application.persistentDataPath + "/save.dat";
        if (!File.Exists(savePath)) { 
            saveData newData = new saveData();
            newData.playerPosition = Vector3.zero;
            SaveGame(newData);
        }
        data = LoadGame();
    }

    void SaveGame(saveData sataToSave) { 
        string JsonData = JsonUtility.ToJson( sataToSave );
        File.WriteAllText( savePath, JsonData );
    }

    saveData LoadGame() { 
        string loadedData = File.ReadAllText( savePath );
        saveData dataToReturn = JsonUtility.FromJson<saveData>( loadedData );   
        return dataToReturn;
    
    }

    public void SaveGameButton() { 
        Vector3 playerPos = playerController.GetPosition();

        data.playerPosition = playerPos;
        SaveGame(data);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Principal");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameButton()
    {
        // Cargar los datos guardados desde el archivo
        data = LoadGame();

        // Cargar la escena del juego de manera asíncrona y restaurar los datos después
        StartCoroutine(LoadGameSceneAndRestoreData());
    }

    private IEnumerator LoadGameSceneAndRestoreData()
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

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
        playerController.SetPosition(data.playerPosition);
    }

}

public class saveData {
    public Vector3 playerPosition;
}

*/


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Queremos guardar la posición del Player
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
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        // Crear una nueva partida
        saveData newData = new saveData();
        newData.playerPosition = Vector3.zero; // Reiniciar la posición
        print("NewGameButton playerPos: " + newData.playerPosition);
        SaveGame(newData);

        // Iniciar el juego en la escena principal
        StartCoroutine(LoadGameSceneAndRestoreData(newData));
    }

    public void ContinueButton()
    {
        // Cargar la escena principal y restaurar la posición del jugador
        StartCoroutine(LoadGameSceneAndRestoreData(data));
    }

    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController esté disponible
        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
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
    public Vector3 playerPosition; // Para almacenar la posición del jugador
    public int currentDay;         // Para almacenar el día actual
    public float gameTimeInMinutes; // Para almacenar el tiempo en minutos
}
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Queremos guardar la posición del Player
    PlayerController playerController;

    string savePath;
    saveData data;

    /*private void Awake()
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
    }*/

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
            newData.currentDay = 0; // Establecer el día en 0 para una nueva partida
            newData.gameTimeInMinutes = 0; // Inicializar el tiempo transcurrido
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

    /*public void NewGameButton()
    {
        // Borrar el archivo de guardado
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        // Crear una nueva partida
        saveData newData = new saveData();
        newData.playerPosition = Vector3.zero; // Reiniciar la posición
        print("NewGameButton playerPos: " + newData.playerPosition);
        SaveGame(newData);

        // Iniciar el juego en la escena principal
        StartCoroutine(LoadGameSceneAndRestoreData(newData));
    }*/

    /*public void NewGameButton()
    {
        // Borrar el archivo de guardado
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        // Crear una nueva partida
        saveData newData = new saveData();
        newData.playerPosition = Vector3.zero; // Reiniciar la posición
        newData.currentDay = 0; // Establecer el día en 0
        newData.gameTimeInMinutes = 0; // Inicializar el tiempo transcurrido
        print("NewGameButton playerPos: " + newData.playerPosition);
        SaveGame(newData);

        // Iniciar el juego en la escena principal
        StartCoroutine(LoadGameSceneAndRestoreData(newData));
    }*/

    public void NewGameButton()
    {
        // Borrar el archivo de guardado
        /*if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        // Crear una nueva partida
        saveData newData = new saveData();
        newData.playerPosition = Vector3.zero; // Reiniciar la posición del jugador
        newData.currentDay = 0; // Establecer el día en 0
        newData.gameTimeInMinutes = 0; // Inicializar el tiempo transcurrido
        SaveGame(newData);

        // Iniciar el juego en la escena principal
        StartCoroutine(LoadGameSceneAndRestoreData(newData));*/
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Principal");
    }

    /* public void ContinueButton()
     {
         // Cargar la escena principal y restaurar la posición del jugador
         StartCoroutine(LoadGameSceneAndRestoreData(data));
     }*/

    public void ContinueButton()
    {
        // Cargar la escena principal y restaurar la posición del jugador
        StartCoroutine(LoadGameSceneAndRestoreData(data));
    }

    /*private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController esté disponible
        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
        }

        // Restaurar la posición del jugador
        print("ContinueButton playerPos: " + dataToRestore.playerPosition);
        playerController.SetPosition(dataToRestore.playerPosition);
    }*/

    /*
    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController y CycleDayController estén disponibles
        while (playerController == null || CycleDayController.instance == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
        }

        // Restaurar la posición del jugador
        print("ContinueButton playerPos: " + dataToRestore.playerPosition);
        playerController.SetPosition(dataToRestore.playerPosition);

        // Restaurar el día y el tiempo en el CycleDayController
        CycleDayController.instance.SetDayAndTime(dataToRestore.currentDay, dataToRestore.gameTimeInMinutes);
    }
    */

    /*private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController y CycleDayController estén disponibles
        while (playerController == null || CycleDayController.Instance == null) // Cambia instance por Instance
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
        }

        // Restaurar la posición del jugador
        print("ContinueButton playerPos: " + dataToRestore.playerPosition);
        playerController.SetPosition(dataToRestore.playerPosition);

        // Restaurar el día y el tiempo en el CycleDayController
        CycleDayController.Instance.SetDayAndTime(dataToRestore.currentDay, dataToRestore.gameTimeInMinutes); // Cambia instance por Instance
    }*/

    /*private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController y CycleDayController estén disponibles
        while (playerController == null || CycleDayController.Instance == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
        }

        // Reiniciar el ciclo del día para una nueva partida
        CycleDayController.Instance.ResetCycle();

        // Restaurar la posición del jugador
        playerController.SetPosition(dataToRestore.playerPosition);

        // Si hay datos específicos para cargar (como días anteriores), puedes aplicarlos aquí si lo necesitas
        CycleDayController.Instance.SetDayAndTime(dataToRestore.currentDay, dataToRestore.gameTimeInMinutes);
    }*/

    private IEnumerator LoadGameSceneAndRestoreData(saveData dataToRestore)
    {
        // Cargar la escena del juego de manera asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Principal");

        // Esperar a que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Esperar a que el PlayerController y CycleDayController estén disponibles
        while (playerController == null || CycleDayController.Instance == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null; // Esperar hasta el siguiente frame
        }

        // Reiniciar el ciclo del día para una nueva partida
        CycleDayController.Instance.ResetCycle();

        // Restaurar la posición del jugador
        playerController.SetPosition(dataToRestore.playerPosition);

        // Si hay datos específicos para cargar (como días anteriores), puedes aplicarlos aquí si lo necesitas
        CycleDayController.Instance.SetDayAndTime(dataToRestore.currentDay, dataToRestore.gameTimeInMinutes);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class saveData
{
    public Vector3 playerPosition; // Para almacenar la posición del jugador
    public int currentDay;         // Para almacenar el día actual
    public float gameTimeInMinutes; // Para almacenar el tiempo en minutos
}