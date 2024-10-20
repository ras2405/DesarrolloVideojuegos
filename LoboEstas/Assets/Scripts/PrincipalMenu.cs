using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{
    public void StopGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
