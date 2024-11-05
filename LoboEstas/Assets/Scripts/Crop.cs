using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    [Header("Animación")]
    private Animator animator;

    [Header("Prefab CarrotPlant")] //Esta es la planta que dropea zanahorias
    [SerializeField] private GameObject carrotPlantPrefab;

    bool isGrowing = true;
    int harvestTimes = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartGrowing();
    }

    void Update()
    {
        int harvestT = 0;
        if (harvestT < harvestTimes) {
            // Comprobamos si la animación "harvested" está en reproducción
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"))
            {
                DropCarrotPlant();
                harvestT++;
            }
        }
    }

    public void StartGrowing() {
       // Debug.Log("Ejecuta la animación (sow)");
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
    /*private void DropCarrotPlant()
    {
        Vector3 currentPosition = transform.position;
        Destroy(gameObject);
        GameObject newCarrot = Instantiate(carrotPlantPrefab, currentPosition, Quaternion.identity);
        Debug.Log("Crop replaced with carrot plant at position: " + currentPosition);
    }*/

    private void DropCarrotPlant() // Deberiamos tener la opcion de dropear dependiendo de la planta cultivada
    {
        Vector3 currentPosition = transform.position;

        // Obtener la posición del tile en el Tilemap
        Vector3Int tilePosition = GameManager.instance.tileManager.interactableMap.WorldToCell(currentPosition);

        // Llamar al método del TileManager para restablecer el tile
        GameManager.instance.tileManager.ResetTile(tilePosition);

        // Destruir la planta actual (recolectada)
        Destroy(gameObject);

        // Crear el prefab de la planta recolectada // ACA CREAMOS LA PLANTA QUE NECESITEMOS!!
        GameObject newCarrot = Instantiate(carrotPlantPrefab, currentPosition, Quaternion.identity);
        //Debug.Log("Crop replaced with carrot plant at position: " + currentPosition);
    }
}
