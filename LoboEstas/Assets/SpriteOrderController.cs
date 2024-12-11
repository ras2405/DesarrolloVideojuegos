using UnityEngine;

public class SpriteOrderController : MonoBehaviour
{
    public SpriteRenderer baseSprite;  // El sprite que ser� la referencia
    public SpriteRenderer targetSprite;  // El sprite que debe estar siempre por delante

    private void Update()
    {
        if (baseSprite != null && targetSprite != null)
        {
            // Asegurarse de que el targetSprite est� siempre un nivel por delante
            targetSprite.sortingOrder = baseSprite.sortingOrder + 1;
        }
    }
}
