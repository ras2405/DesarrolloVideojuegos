using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    private TextMeshProUGUI textMesh; // Reference to the TextMeshProUGUI component
    private float harvested_carrots;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        textMesh.text = harvested_carrots.ToString("Carrots:" + harvested_carrots);
    }

    public void HarvestCarrot(float amount)
    {
        Debug.Log("Se llama a HarvestCarrot con cantidad: " + amount);
        harvested_carrots += amount;
       // UpdateCollectibleDisplay();
    }
}
