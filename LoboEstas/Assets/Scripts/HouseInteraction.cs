using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Text interactText; // Texto que aparece cuando el jugador est� cerca de la puerta
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar
    public bool insideHouse = false; // Para saber si est� dentro de la casa

    private bool canInteract = false; // Si el jugador puede interactuar con la casa

    void Start()
    {
        interactText.gameObject.SetActive(false); // Ocultamos el texto al principio
    }

    void Update()
    {
        // Si el jugador est� cerca y puede interactuar
        if (canInteract)
        {
            interactText.gameObject.SetActive(true); // Mostramos el texto
            if (Input.GetKeyDown(interactionKey)) // Si el jugador presiona la tecla de interacci�n
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
        else
        {
            interactText.gameObject.SetActive(false); // Ocultamos el texto si no est� cerca
        }
    }

    void EnterHouse()
    {
        insideHouse = true;
        player.transform.position = new Vector3(0, 0, 0); // Cambia la posici�n del jugador al interior de la casa (aj�stalo seg�n tu escena)
        Debug.Log("Entraste a la casa");
    }

    void ExitHouse()
    {
        insideHouse = false;
        player.transform.position = new Vector3(5, 5, 0); // Cambia la posici�n del jugador a la granja
        Debug.Log("Saliste de la casa");
    }

    // Detecta si el jugador entra en el �rea de la puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canInteract = true; // Permite que el jugador pueda interactuar
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
