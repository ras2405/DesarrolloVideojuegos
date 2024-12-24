using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    [Header("Animaci�n")]
    private Animator animator;

    [Header("Plant Prefab")] 
    [SerializeField] private GameObject plantPrefab;

    [Header("Riego")]
    [SerializeField] private float wateringCooldown = 5f; 
    private bool isWatered = false;
    private bool isInCooldown = false;


    bool isGrowing = true;
    int growthPhase = 0;
    int harvestTimes = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartGrowing();
    }

    void Update()
    {
        int harvestT = 0;
        if (harvestT < harvestTimes)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("harvested") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("potatoHarvested") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("onionHarvested"))
            {
                DropVegetable();
                harvestT++;
            }
        }
    }

    public void StartGrowing()
    {
        Debug.Log("plantPrefab:" + plantPrefab);
        Debug.Log("Ejecuta la animacion SOW, se planta");
        animator.SetTrigger("sow");
        animator.SetTrigger("sowPotato");
        animator.SetTrigger("sowOnion");
        print("Star growing, Ejecuta la animacion carrotGrowth0");
    }


    public void StartGrow()
    {
        print("Planta inicia animacion: carrotGrowth0");
        isGrowing = true;
    }

    public void StartGrow1()
    {
        print("Planta inicia animacion: carrotGrowth1");
        isWatered = false;
    }

    public void FinishGrow()
    {
        print("Finish growing");
        isGrowing = false;
    }

    public void EndGrow0()
    {
        growthPhase = 1;
        animator.SetInteger("growthPhase", 1);
        print("Planta termina animacion: carrotGrowth0. growthPhase: " + growthPhase);
    }

    public void EndGrow1()
    {
        growthPhase = 2;
        animator.SetInteger("growthPhase", 2);
        print("Planta termina animacion: carrotGrowth1. growthPhase: " + growthPhase);
    }

    public void WaterPlant()
    {
        if (!isWatered)
        {
            isWatered = true;
            animator.SetTrigger("watered"); 
            Debug.Log("La planta ha sido regada. Se ejecuto trigger de watered y growthPhase: " + growthPhase);
        }
        else
        {
            Debug.Log("Esta planta ya está regada.");
        }

    }

    private IEnumerator StartWateringCooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(wateringCooldown);
        isInCooldown = false; 
        Debug.Log("El periodo de enfriamiento ha terminado. Puedes regar la planta nuevamente.");
    }

    private void DropVegetable()
    {
        Vector3 currentPosition = transform.position;

        Vector3Int tilePosition = GameManager.instance.tileManager.interactableMap.WorldToCell(currentPosition);

        GameManager.instance.tileManager.ResetTile(tilePosition);

        Destroy(gameObject);

        GameObject newCarrot = Instantiate(plantPrefab, currentPosition, Quaternion.identity);
        Debug.Log("DropVegetable: Crop replaced with vegetable at position: " + currentPosition);
    }
}