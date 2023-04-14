using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => _cardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _cardHolder.isDrag = true;

        _cardHolder.selectedCard = this;

        // 이동 시 현재 이동하는 코루틴 정지
        ClearCoroutine();

        transform.SetAsLastSibling();

        _bezierCurve.gameObject.SetActive(true);
        _cardHolder.MoveCenter(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _bezierCurve.p0.position = transform.position;
        _bezierCurve.p2.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _cardHolder.isDrag = false;

        // 타겟이 있을 때만 사용
        if (BattleManager.Instance.TargetEnemy != null)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // 때리고 나면 적 null처리
        BattleManager.Instance.TargetEnemy = null;

        // 할거 다 하고 null처리
        _cardHolder.selectedCard = null;

        // 재정렬 후 베지어 곡선 비활성화
        _cardHolder.Relocation();
        _bezierCurve.gameObject.SetActive(false);
    }


    protected override bool Use()
    {
        bool isUsable = base.Use();

        if (isUsable)
        {
            BattleManager.Instance.Player.Act();
            BattleManager.Instance.TargetEnemy.Hit(5);
        }

        return isUsable;
    }
}
