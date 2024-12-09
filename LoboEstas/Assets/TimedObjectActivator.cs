using UnityEngine;
using System.Collections;

public class TimedObjectActivator : MonoBehaviour
{
    public GameObject targetObject;  // El GameObject que quieres mostrar/ocultar
    public float waitTime = 10f;     // Tiempo de espera antes de mostrar el objeto
    public float displayTime = 2f;   // Tiempo durante el cual el objeto estará visible

    private float timer = 0f;        // Temporizador para contar el tiempo transcurrido
    private bool objectActive = false;  // Para saber si el objeto está activo o no

    void Update()
    {
        // Sumar el tiempo transcurrido
        timer += Time.deltaTime;

        // Si han pasado más de 10 segundos, activar el ciclo
        if (timer >= waitTime)
        {
            if (!objectActive)
            {
                // Activar el GameObject
                targetObject.SetActive(true);
                objectActive = true;

                // Iniciar la corutina para ocultar el objeto después de 'displayTime' segundos
                StartCoroutine(HideObjectAfterDelay());
            }
        }
    }

    // Coroutine para desactivar el objeto después de un par de segundos
    private IEnumerator HideObjectAfterDelay()
    {
        yield return new WaitForSeconds(displayTime); // Esperar por 'displayTime' segundos
        targetObject.SetActive(false); // Desactivar el objeto

        // Restablecer el temporizador para que el ciclo se repita
        timer = 0f;
        objectActive = false;
    }
}
