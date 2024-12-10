using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireController : MonoBehaviour
{

    public GameObject fire;
    public GameObject textPanel;
    public TextMeshProUGUI textMeshProUGUI;
    private bool inRange = false;
    private bool fireOff = true;



    void Start()
    {
        Debug.Log("funcion start");
        textPanel.SetActive(false);
    }

    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            LightFire();
        }
    }

    private void LightFire()
    {
        if(GameManager.instance.inventoryContainer.selectedItem != null && GameManager.instance.inventoryContainer.selectedItem.name == "Match")
        {
            fire.SetActive(true);
            GameManager.instance.inventoryContainer.Remove(GameManager.instance.inventoryContainer.selectedItem);
            GameManager.instance.inventoryContainer.selectedItem = null;
            fireOff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && fireOff) // Verifica que el jugador ha entrado
        {
           textPanel.SetActive(true);
           if(GameManager.instance.inventoryContainer.HasItem("Match"))
           {
            textMeshProUGUI.text = "Prende el fuego con tu fósforo presionando la E";
           }
           inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador ha salido
        {
           textPanel.SetActive(false);
           inRange = false;
        }
    }
}
