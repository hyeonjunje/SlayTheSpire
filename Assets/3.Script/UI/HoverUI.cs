using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Sound.PlaySE(ESE.UIClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Sound.PlaySE(ESE.UIHover);
    }
}
