using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseInteraction : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public bool insideHouse = false; // Para saber si está dentro de la casa
    public Vector2 housePos = new Vector2(-25f, -2);
    public Vector2 forestPos = new Vector2(1f, 1f);
    private bool canInteract = false; // Si el jugador puede interactuar con la casa

    void Update()
    {
        // Si el jugador está cerca y puede interactuar
        if (canInteract)
        {
            if (!insideHouse)
            {
                StartCoroutine(TeleportPlayer(housePos));
            }
            else
            {
                StartCoroutine(TeleportPlayer(forestPos));
            }
        }
    }

    void EnterHouse()
    {
        insideHouse = true;
        TeleportPlayer(housePos);

        Debug.Log("Entraste a la casa");
    }

    void ExitHouse()
    {
        insideHouse = false;

        TeleportPlayer(forestPos);
        Debug.Log("Saliste de la casa");
    }

    IEnumerator TeleportPlayer(Vector2 targetPosition)
    {
        Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.enabled = false; // Desactiva la cámara momentáneamente

        yield return new WaitForEndOfFrame(); // Espera un frame

        player.transform.position = targetPosition; // Teletransporta al jugador

        virtualCamera.enabled = true; // Reactiva la cámara
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
