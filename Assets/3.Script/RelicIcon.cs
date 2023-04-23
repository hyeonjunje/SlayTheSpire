using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RelicData _relicData;
    public RelicData RelicData => _relicData;

    public void Init(RelicData relicData)
    {
        _relicData = relicData;
        GetComponent<Image>().sprite = _relicData.relicImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UI.ShowTipUI(_relicData.relicName, _relicData.relicExplanation, ETipPos.Right, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.UI.HideTipUI();
    }
}
