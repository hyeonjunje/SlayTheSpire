using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private RelicData _relicData;
    public RelicData RelicData => _relicData;

    public System.Action onClickRelic;

    public System.Action<Vector3> onPointerEnter;

    public void Init(RelicData relicData)
    {
        _relicData = relicData;
        GetComponent<Image>().sprite = _relicData.relicImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(transform.position);

        GameManager.UI.ShowTipUI(_relicData.relicName, _relicData.relicExplanation, ETipPos.DownRight, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.UI.HideTipUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickRelic?.Invoke();
    }
}
