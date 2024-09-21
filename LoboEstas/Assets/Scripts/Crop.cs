using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    [Header("Animaci�n")]
    private Animator animator;

    [Header("Prefab CarrotPlant")] //Esta es la planta que dropea zanahorias
    [SerializeField] private GameObject carrotPlantPrefab;

   // [Header("Tilemap Interactuable")]
  // [SerializeField] private Tilemap interactableMap; // Tilemap donde se instanciar� el vegetal

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
            // Comprueba si la animaci�n "harvested" est� en reproducci�n
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"))
            {
                DropCarrotPlant();
                harvestT++;
            }
        }
    }

    public void StartGrowing() {
        Debug.Log("Ejecuta la animaci�n (sow)");
        animator.SetTrigger("sow");
    }

    public void StartGrow()
    {
        print("Star growing");
        isGrowing = true;
    }
    public void FinishGrow()
    {
        print("Finish growing");
        isGrowing = false;
    }
    private void DropCarrotPlant()
    {
        // Guarda la posici�n actual
        Vector3 currentPosition = transform.position;

        // Elimina el objeto actual
        Destroy(gameObject);

        // Instancia el prefab de carrotPlant en la posici�n actual
        Instantiate(carrotPlantPrefab, currentPosition, Quaternion.identity);
        Debug.Log("Crop replaced with carrot plant at position: " + currentPosition);
    }


}


/*
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{
    [Header("Animaci�n")]
    private Animator animator;
    private bool hasSown = false;

    [Header("Prefab del Vegetal")]
    [SerializeField] private GameObject carrotPrefab; 

    [Header("Tilemap Interactuable")]
    [SerializeField] private Tilemap interactableMap; // Tilemap donde se instanciar� el vegetal

    bool isGrowing = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartGrowing());
    }

    private IEnumerator StartGrowing()
    {
        Debug.Log("Ejecuta la animaci�n (sow)");
        animator.SetTrigger("sow");

        
        Debug.Log("Espera hasta que la animaci�n (sow) termine");
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("sow") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        Debug.Log("Pasa a la animaci�n (harvested)");
        animator.SetTrigger("harvested");

        Debug.Log("Espera a que termine la animaci�n (harvested)");
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"));

        Debug.Log("Determina la posici�n en el Tilemap  en 0,0,0");
        Vector3Int tilePosition = new Vector3Int(0, 0, 0); // Cambia esto a la celda donde quieras instanciar el vegetal   

        if (interactableMap.HasTile(tilePosition))
        {
            // Convierte la posici�n del Tilemap a coordenadas del mundo
            Vector3 worldPosition = interactableMap.GetCellCenterWorld(tilePosition);
            // Instancia el prefab del vegetal en la posici�n del Tilemap
            Instantiate(carrotPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay un tile en la posici�n especificada del Tilemap.");
        }
    }

    public void StartGrow()
    {
        print("Star growing");
        isGrowing = true;
    }
    public void FinishGrow()
    {
        print("Finish growing");
        isGrowing = false;
    }

}*/