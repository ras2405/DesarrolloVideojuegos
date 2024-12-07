using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantTutorial : MonoBehaviour
{
    public GameObject textPanel; 
    public TextMeshProUGUI displayText;

    void Start()
    {
        textPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador ha entrado
        {
           textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador ha salido
        {
           textPanel.SetActive(false);
        }
    }

    
}
