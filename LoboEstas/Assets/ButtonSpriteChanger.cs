using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Necesario para trabajar con eventos del puntero

public class ButtonSpriteChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite;   // El sprite normal del botón
    public Sprite clickedSprite;  // El sprite cuando el botón es presionado
    private Image buttonImage;    // Referencia al componente Image del botón

    void Start()
    {
        // Obtén el componente Image del botón al inicio
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;  // Establece el sprite normal por defecto
    }

    // Este método se llama cuando el puntero hace clic en el botón (Pointer Down)
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = clickedSprite;  // Cambia al sprite de clic
    }

    // Este método se llama cuando el puntero deja de hacer clic en el botón (Pointer Up)
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;  // Vuelve al sprite normal
    }
}
