using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTutorial : MonoBehaviour
{
    public GameObject textPanel;
    public bool inWaterZone = false;

    void Start()
    {
        inWaterZone = false;
        textPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador ha entrado
        {
            inWaterZone = true;
            textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador ha salido
        {
            inWaterZone = false;
            textPanel.SetActive(false);
        }
    }
}
