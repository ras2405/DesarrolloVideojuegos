/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    [Header("Animacion")]
    private Animator animator;

    [Header("Prefab Plant")] //Esta es la planta que dropea zanahorias
    [SerializeField] private GameObject plantPrefab;

    bool isGrowing = true;
    int harvestTimes = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartGrowing();
        animator.SetTrigger("sowPotato");
        animator.SetTrigger("sowOnion");
    }

    void Update()
    {
        int harvestT = 0;
        if (harvestT < harvestTimes) {
            // Comprobamos si la animaci�n "harvested" est� en reproducci�n
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"))
            {
                DropCarrotPlant();
                harvestT++;
            }
        }
    }

    public void StartGrowing() {
       // Debug.Log("Ejecuta la animaci�n (sow)");
        animator.SetTrigger("sow");
    }

    public void StartGrow()
    {
        // print("Star growing");
        isGrowing = true;
    }
    public void FinishGrow()
    {
        // print("Finish growing");
        isGrowing = false;
    }


    private void DropCarrotPlant()
    {
        Vector3 currentPosition = transform.position;

        // Obtener la posici�n del tile en el Tilemap
        Vector3Int tilePosition = GameManager.instance.tileManager.interactableMap.WorldToCell(currentPosition);

        // Llamar al m�todo del TileManager para restablecer el tile
        GameManager.instance.tileManager.ResetTile(tilePosition);

        // Destruir la planta actual (recolectada)
        Destroy(gameObject);

        // Crear el prefab de la planta recolectada (opcional si la quieres reemplazar)
        GameObject newCarrot = Instantiate(carrotPlantPrefab, currentPosition, Quaternion.identity);
        Debug.Log("Crop replaced with carrot plant at position: " + currentPosition);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    [Header("Animaci�n")]
    private Animator animator;

    [Header("Plant Prefab")] //Esta es la planta que dropea zanahorias
    [SerializeField] private GameObject plantPrefab;

    bool isGrowing = true;
    bool isWatered = false;
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
            // Comprobamos si la animaci�n "harvested" est� en reproducci�n
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
            animator.SetTrigger("watered"); // Activa el trigger en el Animator.
            Debug.Log("La planta ha sido regada. Se ejecuto trigger de watered y growthPhase: " + growthPhase);
        }
        else
        {
            Debug.Log("Esta planta ya está regada.");
        }

    }

    private void DropVegetable()
    {
        Vector3 currentPosition = transform.position;

        // Obtener la posici�n del tile en el Tilemap
        Vector3Int tilePosition = GameManager.instance.tileManager.interactableMap.WorldToCell(currentPosition);

        // Llamar al m�todo del TileManager para restablecer el tile
        GameManager.instance.tileManager.ResetTile(tilePosition);

        // Destruir la planta actual (recolectada)
        Destroy(gameObject);

        // Crear el prefab de la planta recolectada (opcional si la quieres reemplazar)
        GameObject newCarrot = Instantiate(plantPrefab, currentPosition, Quaternion.identity);
        Debug.Log("DropVegetable: Crop replaced with vegetable at position: " + currentPosition);
    }
}