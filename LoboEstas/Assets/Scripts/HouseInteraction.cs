using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public bool insideHouse = false; // Para saber si está dentro de la casa

    private bool canInteract = false; // Si el jugador puede interactuar con la casa

    void Update()
    {
        // Si el jugador está cerca y puede interactuar
        if (canInteract)
        {
            if (!insideHouse)
            {
                EnterHouse(); // Lógica para entrar a la casa
            }
            else
            {
                ExitHouse(); // Lógica para salir de la casa
            }
        }
    }

    void EnterHouse()
    {
        insideHouse = true;
        SceneManager.LoadScene("House");
        Debug.Log("Entraste a la casa");
    }

    void ExitHouse()
    {
        insideHouse = false;
        SceneManager.LoadScene("Principal");
        Debug.Log("Saliste de la casa");
    }

    // Detecta si el jugador entra en el área de la puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = true; // Permite que el jugador pueda entrar a la casa
        }
    }

    // Detecta si el jugador sale del área de la puerta
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = false; // Impide que el jugador interactúe
        }
    }
}
