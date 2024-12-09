using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Necesario para trabajar con eventos del puntero

public class ButtonSpriteChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite;   // El sprite normal del bot�n
    public Sprite clickedSprite;  // El sprite cuando el bot�n es presionado
    private Image buttonImage;    // Referencia al componente Image del bot�n

    void Start()
    {
        // Obt�n el componente Image del bot�n al inicio
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;  // Establece el sprite normal por defecto
    }

    // Este m�todo se llama cuando el puntero hace clic en el bot�n (Pointer Down)
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = clickedSprite;  // Cambia al sprite de clic
    }

    // Este m�todo se llama cuando el puntero deja de hacer clic en el bot�n (Pointer Up)
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;  // Vuelve al sprite normal
    }
}
