using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private TextMeshProUGUI textMesh; // Reference to the TextMeshProUGUI component
    private float harvested_carrots;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        //harvested_carrots += Time.deltaTime;
      //  HarvestCarrot(10);
        UpdateCollectibleDisplay();
    }

    private void UpdateCollectibleDisplay()
    {
        textMesh.text = harvested_carrots.ToString("Carrots:" + harvested_carrots);
    }

    public void HarvestCarrot(float amount)
    {
        harvested_carrots += amount;
    }
}
