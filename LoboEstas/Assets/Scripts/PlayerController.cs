using System;
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

    Vector2 movementInput; // 2 valores, X,Y (izq der, arriba abajo)
    SpriteRenderer SpriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>(); //En que direccion inciia el sprite
    }

    private void FixedUpdate()
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
        else {
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
        //print("Fire pressed");
        animator.SetTrigger("attack");
    }

}
