/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header("Animacion")]
    Animator animator;
    bool harvested = false;
    bool sow = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        harvested = false;
        sow = true;
        StartGrowing(sow, harvested);
    }

    void Update()
    {

    }

    public void StartGrowing(bool sow, bool harvested) {
        animator.SetBool("sowB", sow);
        animator.SetBool("harvestedB", harvested);
        Debug.Log("sow: " + sow);
        Debug.Log("harvested: " + harvested);
        
    }

}
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{
    [Header("Animaci�n")]
    private Animator animator;
    private bool hasSown = false;

    [Header("Prefab del Vegetal")]
    [SerializeField] private GameObject carrotPrefab; // Prefab del vegetal

    [Header("Tilemap Interactuable")]
    [SerializeField] private Tilemap interactableMap; // Tilemap donde se instanciar� el vegetal

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartGrowing());
    }

    private IEnumerator StartGrowing()
    {
        // Ejecuta la animaci�n "sow"
        animator.SetTrigger("sow");

        // Espera hasta que la animaci�n "sow" termine
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("sow"));

        // Pasa a la animaci�n "harvested"
        animator.SetTrigger("harvested");

        // Espera a que termine la animaci�n "harvested"
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"));

        // Determina la posici�n en el Tilemap (cambia esto seg�n tu l�gica)
        Vector3Int tilePosition = new Vector3Int(0, 0, 0); // Cambia esto a la celda donde quieras instanciar el vegetal

        // Aseg�rate de que la celda sea v�lida
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
}