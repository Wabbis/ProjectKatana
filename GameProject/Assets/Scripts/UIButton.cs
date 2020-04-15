using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Vector3 cachedScale;
    public float expandScale;

    void Start()
    {

        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.localScale = new Vector3(expandScale, expandScale, expandScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.localScale = cachedScale;
    }
}
