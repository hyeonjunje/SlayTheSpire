using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TipDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string title;
    [SerializeField]
    [Multiline(10)]
    private string contents;
    [SerializeField]
    private ETipPos tipPos;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UI.ShowTipUI(title, contents, tipPos, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.UI.HideTipUI();
    }
}
