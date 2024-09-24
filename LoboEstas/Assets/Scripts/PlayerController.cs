using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip wateringSound; // Clip de sonido que se reproducirá

    Vector2 movementInput; // 2 valores, X,Y (izq der, arriba abajo)
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;


    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no está inicializado");
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //En que direccion inciia el sprite
        
    }

    void Update()
    {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
        /*  if (canMove)
          {
              if (movementInput != Vector2.zero)
              {
                  bool success = TryMove(movementInput);
                  // Debug.Log("Success: " + success.ToString());
                  if (!success) //&& movementInput.x > 0
                  {
                      success = TryMove(new Vector2(movementInput.x, 0));
                  }

                  if (!success) //&& movementInput.y > 0
                  {
                      success = TryMove(new Vector2(0, movementInput.y));
                  }

                  animator.SetBool("isMovingR", success);
                  //  Debug.Log("isMovingR: " + success.ToString());

              }
              else
              {
                  animator.SetBool("isMovingR", false);
                  //  Debug.Log("isMovingR: " + false.ToString());
              }
              // Podemos hacer flop del sprite para cambiar de direccion!!! movement direction
              if (movementInput.x < 0)
              {
                  SpriteRenderer.flipX = true;
              }
              else if (movementInput.x > 0)
              {
                  SpriteRenderer.flipX = false;
              }
          }*/

       /* //Interaccion con suelo
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {//movementInput.GetKeyDown(KeyCode.Space)
            Debug.Log("Se preciono Space");
            Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            if (GameManager.instance.tileManager.IsInteractable(position))
            {
                Debug.Log("Tile is interactable");
            }
        }*/

    }

    private void HandleMovement()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("isMovingR", success);

            // Flipping the sprite based on movement direction
            spriteRenderer.flipX = movementInput.x < 0;
        }
        else
        {
            animator.SetBool("isMovingR", false);
        }
    }


    private void HandleInteraction()
    {
        Vector3Int position = MapPositionInteractiveTilemap();

        //CULTIVAR LA PLANTA
        if (Keyboard.current.spaceKey.wasPressedThisFrame) // Usa la tecla 'SPACE' para cultivar
        {   
            // Debug.Log("Se presionó Space");
            // Debug.Log("position Vector2 personaje: " + transform.position.x + " , " + transform.position.y);
            //Vector3Int position = new Vector3Int(((int)transform.position.x), (int)transform.position.y, 0);
            // Debug.Log("position Vector3 buscada en tilemapInteractive: "+ position.x + " , " + position.y + " , " + 0);

            if (GameManager.instance.tileManager.IsInteractable(position))
            {
             //   Debug.Log("Player Controller: Tile is interactable2");
                GameManager.instance.tileManager.SetInteracted(position);
            }
        }

        //REGAR LA PLANTA
        if (Keyboard.current.rKey.wasPressedThisFrame) // Usa la tecla 'R' para regar
        {
            if (GameManager.instance.tileManager.IsPlanted(position))
            {
                animator.SetTrigger("watering");
                GameManager.instance.tileManager.WaterPlant(position);

                if (audioSource != null && wateringSound != null)
                {
                    audioSource.PlayOneShot(wateringSound);
                }
            }
            else {
                Debug.Log("Ese tile no fue plantado");
            }
        }
    }


    private Vector3Int MapPositionInteractiveTilemap() {
        //TODO: Por ahora solo mapea a los tiles de cultivo fijos, generalizar
        double posx = transform.position.x;
        double posy = transform.position.y;
        double posx_f = 0;
        double posy_f = 0;
        double posx_i = -2.554902;//0.981;
        double posy_i = -2.858896;//-0.490;
        double tileLength = 0.64;//0.155; //Medida reportada por el player
        int separacionCuadrante_x = 1; //Los tiles estan pegados
        int separacionCuadrante_y = 1; //Los tiles estan separados por uno
        int cuadrante_x = 0;
        int cuadrante_y = 0;
        int filas = 2;
        int columnas = 8;


        if (posx >= posx_i && posx <= posx_i + tileLength)
        {
           // Debug.Log("posy = " + (-4));
            posx_f = -4;
        }
        else if (posx >= posx_i + tileLength && posx <= posx_i + 2 * tileLength)
        {
           // Debug.Log("posy = " + (-3));
            posx_f = -3;
        }
        else if (posx >= posx_i + 6 * tileLength && posx <= posx_i + 7 * tileLength)
        {
           // Debug.Log("posy = " + (2));
            posx_f = 2;
        }
        else if (posx >= posx_i + 7 * tileLength && posx <= posx_i + 8 * tileLength)
        {
          //  Debug.Log("posy = " + (3));
            posx_f = 3;
        }

        if (posy >= posy_i && posy <= posy_i + tileLength)
        {
           // Debug.Log("posy = " + (-5));
            posy_f = -5;
        }
        else if (posy >= posy_i + tileLength && posy <= posy_i + 2 * tileLength)
        {
           // Debug.Log("posy = " + (-4));
            posy_f = -4;
        }
        /*  for (int i = 1; i < (columnas + 1); i++)
          {
              if (posx >= posx_i + ((i - 1) * tileLength) && posx <= posx_i + (i * tileLength))
              {
                  Debug.Log("posx = " + i);
                  posx_f = i;
              }
          }
          for (int i = 0; i < filas; i++)
          {
              if (posy >= posy_i + (separacionCuadrante_y * i * tileLength) && posy <= posy_i + (separacionCuadrante_y * i * tileLength) + tileLength)
              {
                  Debug.Log("posy = " + (2 * i - 5));
                  posy_f = 2 * i - 5;
              }
          }*/
        return new Vector3Int(((int)posx_f), ((int)posy_f), 0);
    }

    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    //Boton derecho presionado para atacar (En nuestro caso podria servir como boton de accion)
    void OnFire() {
        print("Se hizo clic");
        animator.SetTrigger("attack");
        // Si se hace clic sobre un tile y este es interactivo => el tile debe regarce

    }

    //Si no queremos que pueda caminar mientras ataque o riegue en nuestro caso
    public void LockMovement() {
        //print("Lock movement");
        canMove = false;
    }
    public void UnlockMovement() {
        //print("Unlock movement");
        canMove = true;
    }



}
