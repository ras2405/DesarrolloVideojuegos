using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    private float target_poseX;
    private float target_poseY;
    private float posX;
    private float posY;
    public float derechaMax;
    public float izquierdaMax;
    public float alturaMax;
    public float alturaMin;
    public float speed;
    public bool encendida = true;

    //Posicion de la camara al inicio del juego
    void Awake()
    {
        posX = target_poseX + derechaMax;
        posY = target_poseY + alturaMin;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);

    }

    //Posicion de la camara con el movimiento
    void Move_Cam()
    {
        if (encendida)
        {
            if (target)
            { //si target activo
                target_poseX = target.transform.position.x;
                target_poseY = target.transform.position.y;
                if (target_poseX > derechaMax && target_poseX < izquierdaMax)
                {
                    posX = target_poseX;
                }
                if (target_poseY < alturaMax && target_poseY > alturaMin)
                {
                    posY = target_poseY;
                }
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move_Cam();
    }
}
