using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // Reference to the TextMeshProUGUI component
    public float harvested_carrots;

    void Start()
    {
       // Debug.LogError("Inventario: ");
        harvested_carrots = 0;
       // Debug.Log("harvested_carrots actual inicio: " + harvested_carrots);
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        /*if (harvested_carrots != 0) {
            Debug.Log("Se llama para actualizar: " + harvested_carrots);
        }*/
        textMesh.text = harvested_carrots.ToString("Carrots:" + harvested_carrots.ToString());
    }

    public void HarvestCarrot(float amount)
    {
       // Debug.Log("harvested_carrots actual A: " + harvested_carrots);
        Debug.Log("Se llama a HarvestCarrot con cantidad: " + amount);
        harvested_carrots += amount;
        //Debug.Log("harvested_carrots actual B: " + harvested_carrots);
        // UpdateCollectibleDisplay();
    }
}
