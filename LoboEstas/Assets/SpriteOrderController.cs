using UnityEngine;

public class SpriteOrderController : MonoBehaviour
{
    public SpriteRenderer baseSprite;  
    public SpriteRenderer targetSprite; 

    private void Update()
    {
        if (baseSprite != null && targetSprite != null)
        {
            targetSprite.sortingOrder = baseSprite.sortingOrder + 1;
        }
    }
}
