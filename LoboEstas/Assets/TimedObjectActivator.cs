using UnityEngine;
using System.Collections;

public class TimedObjectActivator : MonoBehaviour
{
    public GameObject targetObject;  
    public float waitTime = 10f;    
    public float displayTime = 2f;  

    private float timer = 0f;        
    private bool objectActive = false; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            if (!objectActive)
            {
                targetObject.SetActive(true);
                objectActive = true;

                StartCoroutine(HideObjectAfterDelay());
            }
        }
    }

    private IEnumerator HideObjectAfterDelay()
    {
        yield return new WaitForSeconds(displayTime); 

        timer = 0f;
        objectActive = false;
    }
}
