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
    [Header("Animación")]
    private Animator animator;
    private bool hasSown = false;

    [Header("Prefab del Vegetal")]
    [SerializeField] private GameObject carrotPrefab; // Prefab del vegetal

    [Header("Tilemap Interactuable")]
    [SerializeField] private Tilemap interactableMap; // Tilemap donde se instanciará el vegetal

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartGrowing());
    }

    private IEnumerator StartGrowing()
    {
        // Ejecuta la animación "sow"
        animator.SetTrigger("sow");

        // Espera hasta que la animación "sow" termine
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("sow"));

        // Pasa a la animación "harvested"
        animator.SetTrigger("harvested");

        // Espera a que termine la animación "harvested"
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("harvested"));

        // Determina la posición en el Tilemap (cambia esto según tu lógica)
        Vector3Int tilePosition = new Vector3Int(0, 0, 0); // Cambia esto a la celda donde quieras instanciar el vegetal

        // Asegúrate de que la celda sea válida
        if (interactableMap.HasTile(tilePosition))
        {
            // Convierte la posición del Tilemap a coordenadas del mundo
            Vector3 worldPosition = interactableMap.GetCellCenterWorld(tilePosition);
            // Instancia el prefab del vegetal en la posición del Tilemap
            Instantiate(carrotPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay un tile en la posición especificada del Tilemap.");
        }
    }
}