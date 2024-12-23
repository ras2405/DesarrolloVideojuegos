using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;  
    public Vector2 hotspot = new Vector2(0, 0); 
    void Start()
    {
        SetCustomCursor();
    }

    void SetCustomCursor()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
