using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 
    public float harvested_carrots;

    void Start()
    {
        harvested_carrots = 0;
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        textMesh.text = harvested_carrots.ToString("Carrots:" + harvested_carrots.ToString());
    }

    public void HarvestCarrot(float amount)
    {
        Debug.Log("Se llama a HarvestCarrot con cantidad: " + amount);
        harvested_carrots += amount;
    }
}
