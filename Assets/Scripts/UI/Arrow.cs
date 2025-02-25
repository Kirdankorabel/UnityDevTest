using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsPressed { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsPressed = false;
    }
}
