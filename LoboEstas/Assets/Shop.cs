using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject textPanel; 
    public TextMeshProUGUI raulText; 
    public GameObject newShopTemplate;
    private Animator animator;
    

    private bool playerInZone = false; // Variable para saber si el jugador está en la zona

    // Se llama cuando un objeto con un Collider entra en la zona (trigger)
    
    public void Start()
    {
        animator = GetComponent<Animator>();
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
            HideRaulText();
            textPanel.SetActive(false);
            newShopTemplate.gameObject.SetActive(false);
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
        newShopTemplate.gameObject.SetActive(true);
    }

    private void ShowRaulText()
    {
        textPanel.gameObject.SetActive(true); 
    }

    private void HideRaulText()
    {
        textPanel.gameObject.SetActive(false);
    }

    private void Idle()
    {
        while(true)
        {
             new WaitForSeconds(5f);
             animator.SetTrigger("doAnimation");
             new WaitForSeconds(2f);
             animator.SetTrigger("stopAnimation");
        }
    }
}

