using UnityEngine;

public class SpriteOrderController : MonoBehaviour
{
    public SpriteRenderer baseSprite;  // El sprite que será la referencia
    public SpriteRenderer targetSprite;  // El sprite que debe estar siempre por delante

    private void Update()
    {
        if (baseSprite != null && targetSprite != null)
        {
            // Asegurarse de que el targetSprite esté siempre un nivel por delante
            targetSprite.sortingOrder = baseSprite.sortingOrder + 1;
        }
    }
}
