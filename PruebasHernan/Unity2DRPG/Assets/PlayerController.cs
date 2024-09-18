using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    private GameObject currentInteractableObject = null;
    public InventorySpriteScript spriteScript;
    private bool playerInRange;
    public InteractionScript currentIntScript = null;
    public Inventory inventory;
    Vector2 movementInput; // 2 valores, X,Y (izq der, arriba abajo)
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        inventory = GetComponent<Inventory>(); 
    }

    private void FixedUpdate(){

        //Movimiento
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        if(canMove){
            if(movementInput != Vector2.zero){

            bool success = TryMove(movementInput);

            if(!success){
                success = TryMove(new Vector2(movementInput.x,0));
            }

            if(!success){
                success = TryMove(new Vector2(0,movementInput.y));
            }
            if(movementInput.y > 0 && movementInput.x == 0){
                animator.SetBool("isMovingUp",true);
                animator.SetBool("isMoving",false);
                animator.SetBool("isMovingDown",false);
            }
            else if(movementInput.y < 0 && movementInput.x == 0){
                animator.SetBool("isMovingDown",true);
                animator.SetBool("isMoving",false);
                animator.SetBool("isMovingUp",false);
            }
            else{
                animator.SetBool("isMoving",success);
                animator.SetBool("isMovingUp",false);
                animator.SetBool("isMovingDown",false);
            }
            
            }
            else{
            animator.SetBool("isMoving",false);
            animator.SetBool("isMovingUp",false);
            animator.SetBool("isMovingDown",false);
            }

            //Cambiar direccion del sprite segun la direccion en la que se mueva el personaje
            if(movementInput.x < 0){
            spriteRenderer.flipX = true;
            }
            else if(movementInput.x > 0){
             spriteRenderer.flipX = false;
            }
        }

        //Interactuar con objeto
        if(Input.GetKeyDown(KeyCode.E) && currentInteractableObject){
            if(currentIntScript.canAddToInventory){
                Debug.Log("objeto agregado a inventario");
                inventory.AddItem(currentInteractableObject);
                currentInteractableObject.SendMessage("DoInteraction");
                spriteScript.SendMessage("UpdateInventory");
            }
        }
    }




    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
             int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        direction.Normalize();
        if(count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }else{
            return false;
        }
        
        }
        else{
            return false;
        }
       
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();

    }

    void OnFire(){
        animator.SetTrigger("SwordAttack");
    }

    public void SwordAttack(){
        LockMovement();
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        }
        else{
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crop"))
        {
            Debug.Log("Jugador en rango de interacción");
            currentInteractableObject = collision.gameObject;
            currentIntScript = collision.GetComponent<InteractionScript>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crop"))
        {
            Debug.Log("Jugador fuera de rango de interacción");
            currentInteractableObject = null;
            playerInRange = false;
        }
    }

}
