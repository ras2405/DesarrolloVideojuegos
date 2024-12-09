using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;  // El sprite para el cursor
    public Vector2 hotspot = new Vector2(0, 0);  // Posición donde se detectará el clic (ajusta si es necesario)

    void Start()
    {
        // Al iniciar, establece el cursor por defecto
        SetCustomCursor();
    }

    void SetCustomCursor()
    {
        // Cambia el cursor usando la textura personalizada
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    // Método para restaurar el cursor por defecto (opcional)
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
