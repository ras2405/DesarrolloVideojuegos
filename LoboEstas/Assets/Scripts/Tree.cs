/*using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player").transform; // Asegúrate de que tu personaje tenga la etiqueta "Player"
    }

    void Update()
    {
        // Compara la posición Y del jugador con la posición Y del árbol
        if (playerTransform != null)
        {
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador está por debajo del árbol, el árbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Asegúrate de que este valor sea mayor que el del jugador
            }
            else
            {
                // El jugador está por encima del árbol, el árbol debe estar debajo
                spriteRenderer.sortingOrder = -1; // Asegúrate de que este valor sea menor que el del jugador
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
        // Asegúrate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posición Y del jugador con la posición Y del árbol
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador está por debajo del árbol, el árbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Valor mayor que el del jugador
            }
            else
            {
                // El jugador está por encima del árbol, el árbol debe estar debajo
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

        // Obtén el Transform del jugador en la escena
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Asegúrate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posición Y del jugador con la posición Y del árbol
            if (playerTransform.position.y < transform.position.y)
            {
                print("El jugador está por debajo del árbol, el árbol debe estar encima");
                // El jugador está por debajo del árbol, el árbol debe estar encima
                spriteRenderer.sortingOrder = 1; // Valor mayor que el del jugador
            }
            else
            {
                print("El jugador está por encima del árbol, el árbol debe estar debajo");
                // El jugador está por encima del árbol, el árbol debe estar debajo
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
        // Asegúrate de que playerTransform no sea nulo
        if (playerTransform != null)
        {
            // Compara la posición Y del jugador con la posición Y del árbol
            if (playerTransform.position.y < transform.position.y)
            {
                // El jugador está por debajo del árbol, el árbol debe estar delante
                spriteRenderer.sortingOrder = 1; // Valor mayor para el árbol
            }
            else
            {
                // El jugador está por encima del árbol, el árbol debe estar detrás
                spriteRenderer.sortingOrder = -1; // Valor menor para el árbol
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
            // Cambia el sortingOrder según la posición Y del jugador
            if (playerTransform.position.y > transform.position.y)
            {
                spriteRenderer.sortingOrder = 1; // El árbol está delante
            }
            else
            {
                spriteRenderer.sortingOrder = -1; // El árbol está detrás
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
            // Cambia el sortingOrder según la posición Y del jugador y del objeto hijo
            if (playerTransform.position.y > treeBase.position.y)
            {
                print("El árbol está delante");
                spriteRenderer.sortingOrder = 10; // El árbol está delante
            }
            else
            {
                print("El árbol está detrás");
                spriteRenderer.sortingOrder = -5; // El árbol está detrás
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

            // Cambia el sortingOrder según la posición Y del jugador y del objeto hijo
            if (playerTransform.position.y > treeBase.position.y)
            {
                // El jugador está por encima del árbol
                spriteRenderer.sortingOrder = playerSortingOrder - 1; // El árbol está detrás
            }
            else
            {
                // El jugador está por debajo del árbol
                spriteRenderer.sortingOrder = playerSortingOrder + 1; // El árbol está delante
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

            // Verificamos si el jugador está por encima o por debajo del árbol
            bool isPlayerAbove = playerTransform.position.y > treeBase.position.y;

            // Modificamos el sortingOrder según las condiciones dadas
            if (isPlayerAbove)
            {
                // El árbol está detrás del jugador
                if (spriteRenderer.sortingOrder >= playerSortingOrder)
                {
                    spriteRenderer.sortingOrder = playerSortingOrder - 1; // Colocamos el árbol detrás
                }
            }
            else
            {
                // El árbol está delante del jugador
                if (spriteRenderer.sortingOrder <= playerSortingOrder)
                {
                    spriteRenderer.sortingOrder = playerSortingOrder + 1; // Colocamos el árbol delante
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