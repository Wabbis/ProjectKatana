using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlphaControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public GameObject TargetObject;
    public float desiredAlpha;
    private Image image;
    private Color originalColor, desiredColor;

    private void Start()
    {
        image = TargetObject.GetComponent<Image>();
        originalColor = image.color;
        desiredColor = new Color(image.color.r, image.color.g, image.color.b, desiredAlpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = desiredColor;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        image.color = desiredColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        image.color = originalColor;
    }
}
