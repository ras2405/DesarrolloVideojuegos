using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Text interactText; // Texto que aparece cuando el jugador está cerca de la puerta
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar
    public bool insideHouse = false; // Para saber si está dentro de la casa

    private bool canInteract = false; // Si el jugador puede interactuar con la casa

    void Start()
    {
        interactText.gameObject.SetActive(false); // Ocultamos el texto al principio
    }

    void Update()
    {
        // Si el jugador está cerca y puede interactuar
        if (canInteract)
        {
            interactText.gameObject.SetActive(true); // Mostramos el texto
            if (Input.GetKeyDown(interactionKey)) // Si el jugador presiona la tecla de interacción
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
        else
        {
            interactText.gameObject.SetActive(false); // Ocultamos el texto si no está cerca
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
            canInteract = true; // Permite que el jugador pueda interactuar
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
