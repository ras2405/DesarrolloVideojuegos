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
        textMesh = GetComponent<TextMeshProUGUI>();
        harvested_carrots = 0;
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
        Debug.Log("harvested_carrots actual A: " + harvested_carrots);
        Debug.Log("Se llama a HarvestCarrot con cantidad: " + amount);
        harvested_carrots += amount;
        Debug.Log("harvested_carrots actual B: " + harvested_carrots);
        // UpdateCollectibleDisplay();
    }
}
