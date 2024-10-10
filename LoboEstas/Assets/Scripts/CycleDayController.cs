using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CycleDayController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;//Referencia a la luz global
    [SerializeField] private CycleDay[] cyclesDay;//Referencia a la luz global
    [SerializeField] private float timePerCycle;//Referencia a la luz global

    private float actualTimeCycle = 0;
    private float avarageCycle;
    private int actualCycle = 0;
    private int nextCycle = 1;

    //Cambiamos la luz global por el primer ciclo del dia
    private void Start() {
        globalLight.color = cyclesDay[0].cycleColor;
    }

    //Hacemos que el tiempo aumente
    private void Update()
    {
        actualTimeCycle += Time.deltaTime;
        avarageCycle = actualTimeCycle / timePerCycle;

        if (actualTimeCycle >= timePerCycle) {
            Debug.Log("el color actual es: " + actualCycle);
            actualTimeCycle = 0; //comenzar nuevo ciclo
            actualCycle = nextCycle;
            if (nextCycle + 1 > cyclesDay.Length - 1)
            { //estamos en el ultimo ciclo
                nextCycle = 0;
            }
            else { 
                nextCycle++;
            }
        }

        //Cambiar color
        ChangeColor(cyclesDay[actualCycle].cycleColor, cyclesDay[nextCycle].cycleColor);
    }

    private void ChangeColor(Color currentColor, Color nextColor) {
        globalLight.color = Color.Lerp(currentColor, nextColor, avarageCycle);
    }
}
