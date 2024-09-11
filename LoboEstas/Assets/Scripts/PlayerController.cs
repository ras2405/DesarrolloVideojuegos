using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
/*public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public AudioClip footstepSound;
    private CharacterController playerController;
    private AudioSource audioSource;
    private bool isMoving = false;

    public float derechaMax;
    public float izquierdaMax;
    public float alturaMax;
    public float alturaMin;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
        audioSource.clip = footstepSound;
        playerController = GetComponent<CharacterController>();
        if (playerController == null)
        {
            Debug.LogError("CharacterController component missing from this GameObject.");
            return; // Salir si no se encuentra el componente
        }
        Debug.Log("CharacterController successfully assigned.");

    }

    // Update is called once per frame
    void Update()
    {

        bool isHorizontalPressed = Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed;
        bool isVerticalPressed = Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed;
        bool isAnyDirectionPressed = isHorizontalPressed || isVerticalPressed;

        float horizontal = 0.0f;
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1.0f;
            // audioSource.Play();
            //audioSource.Stop();
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
            // audioSource.Play();
            //audioSource.Stop();
        }
        Debug.Log(horizontal);

        float vertical = 0.0f;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
            // audioSource.Play();
            //audioSource.Stop();
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
            //  audioSource.Play();
            //audioSource.Stop();
        }
        Debug.Log(vertical);

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized * moveSpeed;
        Debug.Log("playerController.isGrounded " + playerController.isGrounded);

        playerController.Move(moveDirection * Time.deltaTime);

        // Verificar si el personaje está en movimiento
        if (moveDirection.magnitude > 0 && !isMoving)
        {
            isMoving = true;
            audioSource.Play(); // Reproduce el sonido de paso
        }
        else if (moveDirection.magnitude == 0 && isMoving)
        {
            isMoving = false;
            audioSource.Stop(); // Detiene el sonido de paso
        }

        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;
    }
}*/


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float delay;
    private float[] walking;
    private Animator ani;
    private RaycastHit2D hit;
    public LayerMask obstaculo;
    private Vector3 dir;
    public Vector3 v3;
    private Vector3 pose;
    public AudioClip footstepSound;
    //private CharacterController playerController;
    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Start()
    {
        dir = new Vector2(0, -1); //mirar al frente
        ani = GetComponent<Animator>();
        walking = new float[4]; // 4 direcciones
        pose = transform.position;

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
        audioSource.clip = footstepSound;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;  // Evita la rotación
    }

    bool CheckCollision {
        get {
            hit = Physics2D.Raycast(transform.position + v3, dir, 1, obstaculo);
            return hit.collider != null;// retornamos cuando colisiona con algo
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, dir);
    }

    public void Move_() {
        //Caminar hacia arriba
        if (Input.GetKey(KeyCode.W))
        { //al apretar w el personaje debe moverse hacia arriba
            dir = new Vector2(0, 1);
            walking[0] += 1 * Time.deltaTime;
            if (transform.position == pose)
            {
                ani.SetFloat("movX", 0);
                ani.SetFloat("movY", 1);
                if (!CheckCollision && walking[0] > delay)
                {
                    ani.SetBool("walk", true); //activamos la animacion de caminar
                    pose += dir;
                    audioSource.Play();
                }
            }
        }
        else {
            walking[0] = 0;
        }

        //Caminar hacia abajo
        if (Input.GetKey(KeyCode.S))
        { //al apretar w el personaje debe moverse hacia arriba
            dir = new Vector2(0, -1);
            walking[1] += 1 * Time.deltaTime;
            if (transform.position == pose)
            {
                ani.SetFloat("movX", 0);
                ani.SetFloat("movY", -1);
                if (!CheckCollision && walking[1] > delay)
                {
                    ani.SetBool("walk", true); //activamos la animacion de caminar
                    pose += dir;
                    audioSource.Play();
                }
            }
        }
        else
        {
            walking[1] = 0;
        }

        //Caminar hacia la derecha
        if (Input.GetKey(KeyCode.D))
        { //al apretar w el personaje debe moverse hacia arriba
            dir = new Vector2(1, 0);
            walking[2] += 1 * Time.deltaTime;
            if (transform.position == pose)
            {
                ani.SetFloat("movX", 1);
                ani.SetFloat("movY", 0);
                if (!CheckCollision && walking[2] > delay)
                {
                    ani.SetBool("walk", true); //activamos la animacion de caminar
                    pose += dir;
                    audioSource.Play();
                }
            }
        }
        else
        {
            walking[2] = 0;
        }

        //Caminar hacia la izquierda
        if (Input.GetKey(KeyCode.A))
        { //al apretar w el personaje debe moverse hacia arriba
            dir = new Vector2(-1, 0);
            walking[3] += 1 * Time.deltaTime;
            if (transform.position == pose)
            {
                ani.SetFloat("movX",-1);
                ani.SetFloat("movY", 0);
                if (!CheckCollision && walking[3] > delay)
                {
                    ani.SetBool("walk", true); //activamos la animacion de caminar
                    pose += dir;
                    audioSource.Play();
                }
            }
        }
        else
        {
            walking[3] = 0;
        }

        if (transform.position == pose) {
            ani.SetBool("walk", false); // cancelamos la animacion de caminar
            audioSource.Stop();
        }

        transform.position = Vector3.MoveTowards(transform.position, pose, speed * Time.deltaTime);

    }
    //Rigidbody2D

    void Update()
    {
        Move_();
    }
}