using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject buyCarrotTemplate;
    public GameObject buyPotatoTemplate;
    public GameObject buyOnionTemplate;
    public GameObject shopTemplate2;
    public GameObject sellPotatoTemplate;
    public GameObject sellOnionTemplate;

    public GameObject textPanel; 
    public TextMeshProUGUI raulText; 

    
    

    private bool playerInZone = false; // Variable para saber si el jugador está en la zona

    // Se llama cuando un objeto con un Collider entra en la zona (trigger)
    
    public void Start()
    {
        textPanel.SetActive(false);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("juagor en zona tienda");
        // Verifica si el objeto que entra tiene el tag "Player" (puedes cambiar esto según tus necesidades)
        if (other.CompareTag("Player"))
        {
            Debug.Log("entra al if");
            playerInZone = true;
            Debug.Log("Jugador ha entrado en la zona.");
            ShowRaulText();
        }
       
    }

    // Se llama cuando un objeto con un Collider sale de la zona (trigger)
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica si el objeto que sale tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            buyCarrotTemplate.gameObject.SetActive(false);
            shopTemplate2.gameObject.SetActive(false);
            buyPotatoTemplate.gameObject.SetActive(false);
            sellPotatoTemplate.gameObject.SetActive(false);
            buyOnionTemplate.gameObject.SetActive(false);
            sellOnionTemplate.gameObject.SetActive(false);
            textPanel.SetActive(false);
            Debug.Log("Jugador ha salido de la zona.");
        }
    }

    private void Update()
    {
        // Verifica si el jugador está en la zona y presiona la tecla 'E'
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            ExecuteAction();
            HideRaulText();
        }
    }

    // Función que se ejecutará al presionar 'E' en la zona
    private void ExecuteAction()
    {
        buyCarrotTemplate.gameObject.SetActive(true);
        shopTemplate2.gameObject.SetActive(true);
        buyPotatoTemplate.gameObject.SetActive(true);
        sellPotatoTemplate.gameObject.SetActive(true);
        buyOnionTemplate.gameObject.SetActive(true);
        sellOnionTemplate.gameObject.SetActive(true);
    }

    private void ShowRaulText()
    {
        textPanel.gameObject.SetActive(true); 
    }

    private void HideRaulText()
    {
        textPanel.gameObject.SetActive(false);
    }
}

