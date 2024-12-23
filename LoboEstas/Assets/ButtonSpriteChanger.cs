using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ButtonSpriteChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite; 
    public Sprite clickedSprite; 
    private Image buttonImage;  

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;  
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = clickedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite; 
    }
}
