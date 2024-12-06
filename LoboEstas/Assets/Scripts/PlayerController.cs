using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] ItemContainer inventory;
    public float moveSpeed = 1f;
    public float runSpeed = 1.3f;
    public float currentSpeed;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Transform playerBase;

    public AudioClip footstepsSound;
    private float stepCooldown = 0.5f; 
    private float stepTimer;

    public AudioSource audioSource; 
    public AudioClip wateringSound; 
    public AudioClip graveSound;
    public AudioClip collectingSound;
    public AudioClip sowSound;
    public AudioClip fillCanSound;
    public AudioClip eatingSound;

    public GameObject lightNoLamp;
    public GameObject lightWithLamp;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public Animator animator;
    public float tiempoEntreChocadas = 5.0f;
    private float contadorChocadas;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool isRunning = false;
    bool canMove = true;

    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;

    private List<Collider2D> waterZones = new List<Collider2D>();
    private Coroutine waterFillingCoroutine;
    private bool isInWaterZone = false;
    public Image waterBar;
    public float water, maxWater;
    public float waterCost; 
    private bool collectingWater = false;
    private bool eating = false;

    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no está inicializado");
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        stepTimer = 0; 
        contadorChocadas = tiempoEntreChocadas;
        currentSpeed = moveSpeed;

    }

    void Update()
    {
        eating = false;
        animator.SetBool("eating", eating);
        EatCarrot();
        if(Keyboard.current.leftShiftKey.isPressed && stamina > 0)
        {
            isRunning = true;
        }
        else{
            isRunning = false;
        }

        if(isRunning && TryMove(movementInput))
        {
            stamina -= runCost * Time.deltaTime;
            if(stamina < 0)stamina = 0;
            staminaBar.fillAmount = stamina / maxStamina;
        }

        FillWater();
        HandleInteraction();

        if (movementInput == Vector2.zero)
        {
            contadorChocadas -= Time.deltaTime;

        
            if (contadorChocadas <= 0)
            {
                animator.SetBool("isChocandoManos", true);
                contadorChocadas = tiempoEntreChocadas;
            }
            else
            {
                animator.SetBool("isChocandoManos", false);
            }
        }
        else
        {
            animator.SetBool("isChocandoManos", false);
            contadorChocadas = tiempoEntreChocadas;
        }

        if (HasLamp())
        {
            lightNoLamp.gameObject.SetActive(false);
            lightWithLamp.gameObject.SetActive(true);
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

    private bool HasLamp()
    {
        foreach(ItemSlot slot in inventory.slots)
        {
            if (slot.item != null && slot.item.name == "LampWithLight")
            {
                return true;
            }
        }
        return false;
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

            if (success && stepTimer >= stepCooldown)
            {
                audioSource.PlayOneShot(footstepsSound);
                stepTimer = 0; 
            }

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
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Vector3Int position = MapPositionInteractiveTilemap();

            if (GameManager.instance != null && GameManager.instance.tileManager != null)
            {
                if (GameManager.instance.tileManager.IsPlanted(position))
                {

                    if (water > waterCost) 
                    {
                        animator.SetTrigger("watering");
                        GameManager.instance.tileManager.WaterPlant(position);

                        water -= waterCost;
                        if (water < 0) water = 0; 
                        waterBar.fillAmount = water / maxWater;

                        if (audioSource != null && wateringSound != null)
                        {
                            audioSource.PlayOneShot(wateringSound);
                        }

                        Debug.Log("Se regó la planta. Agua restante: " + water);
                    }
                    else
                    {
                        Debug.Log("No tienes suficiente agua para regar la planta.");
                    }
                }
                else if (GameManager.instance.tileManager.IsInteractable(position) && SeedSelected())
                {
                    Debug.Log("Se intenta plantar inventory.selectedItem.name " + inventory.selectedItem.name);
                    if (audioSource != null && sowSound != null)
                    {
                        audioSource.PlayOneShot(sowSound);
                    }
                    animator.SetTrigger("pigSow");
                    GameManager.instance.tileManager.SetInteracted(position, inventory.selectedItem.name);
                    inventory.RemoveSeed();
                    Debug.Log("Se planta la planta xD " + inventory.selectedItem.name);
                }
                else
                {
                    Debug.Log("No se pudo plantar SeedSelected(): " + SeedSelected() + " GameManager.instance.tileManager.IsInteractable(position): " + GameManager.instance.tileManager.IsInteractable(position));
                }
            }
        }
    }

    public bool SeedSelected()
   {
    Debug.Log(inventory.selectedItem);
    if (inventory.selectedItem != null && inventory.selectedItem.tag == "seed")return true;
    return false;
   }

    public void EatCarrot()
    {
        if (!isRunning)
        {
            if (inventory.selectedItem != null)
            {
                if (inventory.selectedItem.tag == "Carrot" && Input.GetMouseButtonDown(1))
                {
                    if (audioSource != null && eatingSound != null)
                    {
                        audioSource.PlayOneShot(eatingSound);
                    }
                    eating = true;
                    animator.SetBool("eating", eating);
                    inventory.Remove(inventory.selectedItem);
                    if (stamina > 60) stamina = 100;
                    else
                    {
                        stamina += 40;
                    }
                    staminaBar.fillAmount = stamina / maxStamina;
                }
            }
        }
    }

    public void FillWater()
    {
        if (isInWaterZone)
        {
            if (Keyboard.current.eKey.isPressed) 
            {
                collectingWater = true; 
                animator.SetBool("collectingWater", collectingWater);
                if (waterFillingCoroutine == null)
                {
                    waterFillingCoroutine = StartCoroutine(FillWaterGradually());
                }
            }
            else if (Keyboard.current.eKey.wasReleasedThisFrame) 
            {
                if (waterFillingCoroutine != null)
                {
                    StopCoroutine(waterFillingCoroutine);
                    waterFillingCoroutine = null;
                }
                collectingWater = false;
                animator.SetBool("collectingWater", collectingWater);
                UnlockMovement();
            }
        }
    }


    private IEnumerator FillWaterGradually()
    {
        while (water < maxWater)
        {
            water += maxWater * 0.05f;
            if (audioSource != null && fillCanSound != null)
            {
                audioSource.PlayOneShot(fillCanSound);
            }
            if (water > maxWater)
            {
                water = maxWater;
            }

            waterBar.fillAmount = water / maxWater; 

            Debug.Log("Agua actual: " + water);

            yield return new WaitForSeconds(0.1f); 
        }

        waterFillingCoroutine = null; 
        Debug.Log("Barra de agua completamente rellenada.");
    }

    private Vector3Int MapPlayerAndInteractableMapPosition()
    { 
        return MapPositionInteractiveTilemap();
    }

    private Vector3Int MapPositionInteractiveTilemap() {
          double posx = transform.position.x;
          double posy = transform.position.y;
          double posx_f = 0;
          double posy_f = 0;
          double posx_i = -2.554902; 
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
                currentSpeed * Time.fixedDeltaTime + collisionOffset 
            );
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * currentSpeed * Time.fixedDeltaTime); 
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterZone"))
        {
            if (!waterZones.Contains(other))
            {
                waterZones.Add(other); 
            }
            UpdateWaterZoneStatus(); 
            Debug.Log($"Entraste en una zona de agua. Total de zonas activas: {waterZones.Count}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WaterZone"))
        {
            if (waterZones.Contains(other))
            {
                waterZones.Remove(other); 
            }
            UpdateWaterZoneStatus(); 
            Debug.Log($"Saliste de una zona de agua. Total de zonas activas: {waterZones.Count}");
        }
    }

    private void UpdateWaterZoneStatus()
    {
        isInWaterZone = waterZones.Count > 0; 
        Debug.Log($"Estado actual de agua: {isInWaterZone}");
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    
    void OnFire() {
        animator.SetTrigger("attack");
    }

    public void LockMovement() {
        canMove = false;
    }
    public void UnlockMovement() {
        canMove = true;
    }

    public Vector2 GetPosition()
    {
        print("GetPosition: " + transform.position);
        return transform.position;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    private void InitializePosition()
    {
        if (GameManager.instance != null && GameManager.instance.saveSystem != null)
        {
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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null)
            {
                InteractWithObject(hit.collider.gameObject);
            }
        }
    }

    private void InteractWithObject(GameObject clickedObject)
    {
        if (clickedObject.CompareTag("Grave"))
        {
            if (audioSource != null && graveSound != null)
            {
                audioSource.PlayOneShot(graveSound);  
                Debug.Log("Se hizo clic en una tumba y se reprodujo el sonido.");
            }
        }
        else
        {
            Debug.Log("Interacción con otro objeto: " + clickedObject.name);
        }
    }

    public void StartAnimationCollect()
    {
        if (!isRunning)
        {
            if (audioSource != null && collectingSound != null)
            {
                audioSource.PlayOneShot(collectingSound);
            }
            animator.SetBool("isMovingR", false);
            animator.SetTrigger("pigCollect");
            LockMovement();
        }
    }

    public void OnCollectAnimationComplete()
    {
        UnlockMovement(); 
    }
}
