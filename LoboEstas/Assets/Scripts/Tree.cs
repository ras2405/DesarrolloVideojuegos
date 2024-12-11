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

            // Verificamos si el jugador est� por encima o por debajo del �rbol
            bool isPlayerAbove = playerBase.position.y < treeBase.position.y;
           // print("isPlayerAbove: " + isPlayerAbove);
           // print("posicion y del arbol: " + treeBase.position.y
            //   + " posicion y del jugador: " + playerBase.position.y);

          //  print("order arbol: "+ spriteRenderer.sortingOrder
           //     + " order jugador: " + playerSortingOrder);
            // Modificamos el sortingOrder seg�n las condiciones dadas
            if (isPlayerAbove)
            {
              //  print("El �rbol est� detr�s del jugador");
                // El �rbol est� detr�s del jugador
                spriteRenderer.sortingOrder = playerSortingOrder - 3;
                //if (spriteRenderer.sortingOrder < playerSortingOrder)
              //  {
              //      spriteRenderer.sortingOrder = playerSortingOrder - 1; // Colocamos el �rbol detr�s
               // }
            }
            else
            {
               // print("El �rbol est� delante del jugador");
                spriteRenderer.sortingOrder = playerSortingOrder + 3;
                // El �rbol est� delante del jugador
                // if (spriteRenderer.sortingOrder >= playerSortingOrder)
                //  {
                //spriteRenderer.sortingOrder = playerSortingOrder + 1; // Colocamos el �rbol delante
               // }
            }
        }
    }
}