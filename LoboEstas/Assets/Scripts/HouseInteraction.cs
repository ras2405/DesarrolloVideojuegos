using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public bool insideHouse = false; // Para saber si est� dentro de la casa

    private bool canInteract = false; // Si el jugador puede interactuar con la casa

    void Update()
    {
        // Si el jugador est� cerca y puede interactuar
        if (canInteract)
        {
            if (!insideHouse)
            {
                EnterHouse(); // L�gica para entrar a la casa
            }
            else
            {
                ExitHouse(); // L�gica para salir de la casa
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

    // Detecta si el jugador entra en el �rea de la puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = true; // Permite que el jugador pueda entrar a la casa
        }
    }

    // Detecta si el jugador sale del �rea de la puerta
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = false; // Impide que el jugador interact�e
        }
    }
}
