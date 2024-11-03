﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] ItemContainer inventory;

    //MARCHA
    //velocidad de caminar y correr
    public float moveSpeed = 1f;
    public float runSpeed = 2f;
    private float currentSpeed;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    //SONIDOS
    //sonido de pasos
    public AudioClip footstepsSound; 
    private float stepCooldown = 0.9f; // Tiempo entre pasos
    private float stepTimer;

    public AudioSource audioSource; 
    public AudioClip wateringSound; 

    Vector2 movementInput; // 2 valores, X,Y (izq der, arriba abajo)
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public Animator animator;
    public float tiempoEntreChocadas = 5.0f;
    private float contadorChocadas;


    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool isRunning = false;
    bool canMove = true;

    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no está inicializado");
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //En que direccion inciia el sprite
     
        stepTimer = 0; // Inicializa el temporizador
        contadorChocadas = tiempoEntreChocadas;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        isRunning = Keyboard.current.leftShiftKey.isPressed;
        HandleInteraction();
        // Verificar si el personaje está quieto
        if (movementInput == Vector2.zero)
        {
            contadorChocadas -= Time.deltaTime;

            // Si el tiempo es suficiente, activa la animación de chocarse las patas
            if (contadorChocadas <= 0)
            {
                animator.SetBool("isChocandoManos", true);
                // Reinicia el contador
                contadorChocadas = tiempoEntreChocadas;
            }
            else
            {
                // Asegúrate de que la animación de idle normal se reproduce cuando no está chocando
                animator.SetBool("isChocandoManos", false);
            }
        }
        else
        {
            // Si el personaje se mueve, asegúrate de que no esté chocando patas
            animator.SetBool("isChocandoManos", false);
            contadorChocadas = tiempoEntreChocadas; // Reinicia el contador
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            DetectMouseClick();
            stepTimer += Time.fixedDeltaTime; 
        }
    }

    private void HandleMovement()
    {
        currentSpeed = isRunning ? runSpeed : moveSpeed; 

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
            animator.SetBool("isRunning", isRunning);

            // Reproducir sonido de pasos
            if (success && stepTimer >= stepCooldown)
            {
                audioSource.PlayOneShot(footstepsSound);
                stepTimer = 0;
            }

            // Flip sprites
            spriteRenderer.flipX = movementInput.x < 0;
        }
        else
        {
            animator.SetBool("isMovingR", false);
            animator.SetBool("isRunning", false);
            audioSource.Stop();
        }
    }


    private void HandleInteraction()
    {
        Vector3Int position = MapPositionInteractiveTilemap();

        if (GameManager.instance != null && GameManager.instance.tileManager != null)
        {

            if (Mouse.current.leftButton.wasPressedThisFrame) // Clic izquierdo del mouse
            {
                //CULTIVAR LA PLANTA 
                if (GameManager.instance.tileManager.IsInteractable(position) && SeedSelected())
                {
                    animator.SetTrigger("pigSow");
                    print("pigSow");
                    GameManager.instance.tileManager.SetInteracted(position);
                    inventory.RemoveSeed();
                }
                else
                {
                    //REGAR LA PLANTA
                    if (GameManager.instance.tileManager.IsPlanted(position))
                    {
                        animator.SetTrigger("watering");
                        GameManager.instance.tileManager.WaterPlant(position);

                        if (audioSource != null && wateringSound != null)
                        {
                            audioSource.PlayOneShot(wateringSound);
                        }
                    }
                    else
                    {
                        if (GameManager.instance.tileManager.IsInteractable(position))
                        {
                            Debug.Log("Ese tile no fue plantado");
                        }
                        else
                        {
                            //Debug.Log("No se puede interactuar con este tile");
                        }
                    }
                }
            }
        }
        else {
            //Debug.Log("No se puede interactuar con este tile");
        }
    }

    public bool SeedSelected()
   {
    Debug.Log(inventory.selectedItem);
    if (inventory.selectedItem != null && inventory.selectedItem.tag == "seed")return true;
    return false;
   }

    private Vector3Int MapPlayerAndInteractableMapPosition()
    { 
    
        return MapPositionInteractiveTilemap();
    }

    private Vector3Int MapPositionInteractiveTilemap() {
          //TODO: Por ahora solo mapea a los tiles de cultivo fijos, generalizar
          double posx = transform.position.x;
          double posy = transform.position.y;
          double posx_f = 0;
          double posy_f = 0;
          double posx_i = -2.554902; //Medida reportada por el player, esquina inferior 
          double posy_i = -2.858896;
          double tileLength = 0.64; 

          if (posx >= posx_i && posx <= posx_i + tileLength)
          {
              posx_f = -4;
          }
          else if (posx >= posx_i + tileLength && posx <= posx_i + 2 * tileLength)
          {
              posx_f = -3;
          }
          else if (posx >= posx_i + 6 * tileLength && posx <= posx_i + 7 * tileLength)
          {
              posx_f = 2;
          }
          else if (posx >= posx_i + 7 * tileLength && posx <= posx_i + 8 * tileLength)
          {
              posx_f = 3;
          }

          if (posy >= posy_i && posy <= posy_i + tileLength)
          {
              posy_f = -5;
          }
          else if (posy >= posy_i + tileLength && posy <= posy_i + 2 * tileLength)
          {
              posy_f = -4;
          }

        return new Vector3Int(((int)posx_f), ((int)posy_f), 0);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                currentSpeed * Time.fixedDeltaTime + collisionOffset // Cambiado aquí
            );
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * currentSpeed * Time.fixedDeltaTime); // Cambiado aquí
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("attack");
    }

    //Si no queremos que pueda caminar mientras ataque o riegue en nuestro caso
    public void LockMovement() {
        print("Lock movement");
        canMove = false;
    }
    public void UnlockMovement() {
        print("Unlock movement");
        canMove = true;
    }

    public Vector2 GetPosition()
    {
        print("GetPosition: " + transform.position);
        return transform.position;//rb.position;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    private void InitializePosition()
    {
        // Este m�todo se llama al inicio del juego
        if (GameManager.instance != null && GameManager.instance.saveSystem != null)
        {
            // Cargar los datos guardados
            saveData data = GameManager.instance.saveSystem.LoadGame();
            if (data != null)
            {
                SetPosition(data.playerPosition);
            }
        }
    }

    private void DetectMouseClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Convertir la posición del mouse a coordenadas del mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            // Hacer un raycast desde la posición del mouse
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null)
            {
                // El raycast ha chocado con algo que tiene un collider
                Debug.Log("Hiciste clic en el objeto: " + hit.collider.name);

                // Aquí puedes añadir cualquier lógica que quieras realizar al hacer clic en el objeto
                InteractWithObject(hit.collider.gameObject);
            }
        }
    }

    private void InteractWithObject(GameObject clickedObject)
    {
        // Añade la lógica para interactuar con el objeto clickeado
        Debug.Log("Interactuando con " + clickedObject.name);
    }

    public void StartAnimationCollect()
    {
        animator.SetBool("isMovingR", false);
        animator.SetTrigger("pigCollect"); // Asegúrate de que este trigger esté definido en tu Animator
        LockMovement(); // Bloquea el movimiento al iniciar la recolección
    }

    public void OnCollectAnimationComplete()
    {
        UnlockMovement(); // Desbloquea el movimiento al finalizar la animación
    }

}


/*private bool TryMove(Vector2 direction) {
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
}*/

/*private void HandleMovement()
{
    float currentSpeed = isRunning ? runSpeed : moveSpeed;

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
        animator.SetBool("isRunning", isRunning);

        // Reproducir sonido de pasos
        if (success && stepTimer >= stepCooldown)
        {
            audioSource.PlayOneShot(footstepsSound);
            stepTimer = 0; 
        }

        // Flip sprites
        spriteRenderer.flipX = movementInput.x < 0;
    }
    else
    {
        animator.SetBool("isMovingR", false);
        animator.SetBool("isRunning", false);
        audioSource.Stop(); 
    }
}*/