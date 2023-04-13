using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => GameManager.Game.CardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;

        // 이동 시 현재 이동하는 코루틴 정지
        ClearCoroutine();

        transform.SetAsLastSibling();

        _bezierCurve.gameObject.SetActive(true);
        _bezierCurve.p0.position = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 카드가 마우스 따라가게
        _bezierCurve.p2.position = eventData.position;
    }

    // 드랍할 수 있는 곳에 드랍할 때
    public void OnDrop(PointerEventData eventData)
    {
        // 사용가능(코스트 등등)이거나 사용범위에 있을 때만 사용
        // 아니면 재정렬

        _isDrag = false;

        _cardHolder.DisplayMyHand();
        _bezierCurve.gameObject.SetActive(false);
    }

    // 둘다 
    public void OnEndDrag(PointerEventData eventData)
    {
        // 사용가능(코스트 등등)이거나 사용범위에 있을 때만 사용
        // 아니면 재정렬

        _isDrag = false;

        _cardHolder.DisplayMyHand();
        _bezierCurve.gameObject.SetActive(false);
    }
}
