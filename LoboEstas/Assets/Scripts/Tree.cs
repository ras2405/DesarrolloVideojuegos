using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    public Transform playerBase; // Referencia al objeto base del jugador

    // Referencia al objeto hijo cuyo transform queremos usar
    public Transform treeBase;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerBase != null && treeBase != null)
        {
            int playerSortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder;

            // Verificamos si el jugador está por encima o por debajo del árbol
            bool isPlayerAbove = playerBase.position.y < treeBase.position.y;
            print("isPlayerAbove: " + isPlayerAbove);
            print("posicion y del arbol: " + treeBase.position.y
               + " posicion y del jugador: " + playerBase.position.y);

            print("order arbol: "+ spriteRenderer.sortingOrder
                + " order jugador: " + playerSortingOrder);
            // Modificamos el sortingOrder según las condiciones dadas
            if (isPlayerAbove)
            {
                print("El árbol está detrás del jugador");
                // El árbol está detrás del jugador
                spriteRenderer.sortingOrder = playerSortingOrder - 1;
                //if (spriteRenderer.sortingOrder < playerSortingOrder)
              //  {
              //      spriteRenderer.sortingOrder = playerSortingOrder - 1; // Colocamos el árbol detrás
               // }
            }
            else
            {
                print("El árbol está delante del jugador");
                spriteRenderer.sortingOrder = playerSortingOrder + 1;
                // El árbol está delante del jugador
                // if (spriteRenderer.sortingOrder >= playerSortingOrder)
                //  {
                //spriteRenderer.sortingOrder = playerSortingOrder + 1; // Colocamos el árbol delante
               // }
            }
        }
    }
}