/*using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player").transform; // Aseg�rate de que tu personaje tenga la etiqueta "Player"
    }

    void Update()
    {
        // Compara la posici�n Y del jugador con la posici�n Y del �rbol
        if (playerTransform != null)
        {
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador est� por debajo del �rbol, el �rbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Aseg�rate de que este valor sea mayor que el del jugador
            }
            else
            {
                // El jugador est� por encima del �rbol, el �rbol debe estar debajo
                spriteRenderer.sortingOrder = -1; // Aseg�rate de que este valor sea menor que el del jugador
            }
        }
    }
}*/
/*
using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Asigna el Transform del jugador desde el Inspector
    public Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Aseg�rate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posici�n Y del jugador con la posici�n Y del �rbol
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador est� por debajo del �rbol, el �rbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Valor mayor que el del jugador
            }
            else
            {
                // El jugador est� por encima del �rbol, el �rbol debe estar debajo
                spriteRenderer.sortingOrder = -1; // Valor menor que el del jugador
            }
        }
    }
}*/
/*using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obt�n el Transform del jugador en la escena
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Aseg�rate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posici�n Y del jugador con la posici�n Y del �rbol
            if (playerTransform.position.y < transform.position.y)
            {
                print("El jugador est� por debajo del �rbol, el �rbol debe estar encima");
                // El jugador est� por debajo del �rbol, el �rbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Valor mayor que el del jugador
            }
            else
            {
                print("El jugador est� por encima del �rbol, el �rbol debe estar debajo");
                // El jugador est� por encima del �rbol, el �rbol debe estar debajo
                spriteRenderer.sortingOrder = -1; // Valor menor que el del jugador
            }
        }
    }
}*/
/*
using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Asigna el transform del jugador en el inicio si es necesario
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Aseg�rate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posici�n Y del jugador con la posici�n Y del �rbol
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador est� por debajo del �rbol, el �rbol debe estar delante
                spriteRenderer.sortingOrder = 1; // Valor mayor para el �rbol
            }
            else
            {
                // El jugador est� por encima del �rbol, el �rbol debe estar detr�s
                spriteRenderer.sortingOrder = -1; // Valor menor para el �rbol
            }
        }
    }
}
*/
/*using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Cambia el sortingOrder seg�n la posici�n Y del jugador
            if (playerTransform.position.y > transform.position.y)
            {
                spriteRenderer.sortingOrder = 1; // El �rbol est� delante
            }
            else
            {
                spriteRenderer.sortingOrder = -1; // El �rbol est� detr�s
            }
        }
    }
}
*/
/*
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    // Referencia al objeto hijo cuyo transform queremos usar
    public Transform treeBase;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerTransform != null && treeBase != null)
        {
            // Cambia el sortingOrder seg�n la posici�n Y del jugador y del objeto hijo
            if (playerTransform.position.y > treeBase.position.y)
            {
                print("El �rbol est� delante");
                spriteRenderer.sortingOrder = 10; // El �rbol est� delante
            }
            else
            {
                print("El �rbol est� detr�s");
                spriteRenderer.sortingOrder = -5; // El �rbol est� detr�s
            }
        }
    }
}*/
/*
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    // Referencia al objeto hijo cuyo transform queremos usar
    public Transform treeBase;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerTransform != null && treeBase != null)
        {
            int playerSortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder;

            // Cambia el sortingOrder seg�n la posici�n Y del jugador y del objeto hijo
            if (playerTransform.position.y > treeBase.position.y)
            {
                // El jugador est� por encima del �rbol
                spriteRenderer.sortingOrder = playerSortingOrder - 1; // El �rbol est� detr�s
            }
            else
            {
                // El jugador est� por debajo del �rbol
                spriteRenderer.sortingOrder = playerSortingOrder + 1; // El �rbol est� delante
            }
        }
    }
}*/
/*
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    // Referencia al objeto hijo cuyo transform queremos usar
    public Transform treeBase;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerTransform != null && treeBase != null)
        {
            int playerSortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder;

            // Verificamos si el jugador est� por encima o por debajo del �rbol
            bool isPlayerAbove = playerTransform.position.y > treeBase.position.y;

            // Modificamos el sortingOrder seg�n las condiciones dadas
            if (isPlayerAbove)
            {
                // El �rbol est� detr�s del jugador
                if (spriteRenderer.sortingOrder >= playerSortingOrder)
                {
                    spriteRenderer.sortingOrder = playerSortingOrder - 1; // Colocamos el �rbol detr�s
                }
            }
            else
            {
                // El �rbol est� delante del jugador
                if (spriteRenderer.sortingOrder <= playerSortingOrder)
                {
                    spriteRenderer.sortingOrder = playerSortingOrder + 1; // Colocamos el �rbol delante
                }
            }
        }
    }
}*/

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
            print("isPlayerAbove: " + isPlayerAbove);
            print("posicion y del arbol: " + treeBase.position.y
               + " posicion y del jugador: " + playerBase.position.y);

            print("order arbol: "+ spriteRenderer.sortingOrder
                + " order jugador: " + playerSortingOrder);
            // Modificamos el sortingOrder seg�n las condiciones dadas
            if (isPlayerAbove)
            {
                print("El �rbol est� detr�s del jugador");
                // El �rbol est� detr�s del jugador
                spriteRenderer.sortingOrder = playerSortingOrder - 1;
                //if (spriteRenderer.sortingOrder < playerSortingOrder)
              //  {
              //      spriteRenderer.sortingOrder = playerSortingOrder - 1; // Colocamos el �rbol detr�s
               // }
            }
            else
            {
                print("El �rbol est� delante del jugador");
                spriteRenderer.sortingOrder = playerSortingOrder + 1;
                // El �rbol est� delante del jugador
                // if (spriteRenderer.sortingOrder >= playerSortingOrder)
                //  {
                //spriteRenderer.sortingOrder = playerSortingOrder + 1; // Colocamos el �rbol delante
               // }
            }
        }
    }
}