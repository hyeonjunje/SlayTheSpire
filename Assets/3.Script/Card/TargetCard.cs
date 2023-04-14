using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => CardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        CardHolder.isDrag = true;

        CardHolder.selectedCard = this;

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


    public void OnEndDrag(PointerEventData eventData)
    {
        CardHolder.isDrag = false;

        // 타겟이 있을 때만 사용
        if (BattleManager.Instance.TargetEnemy != null)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // 때리고 나면 적 null처리
        BattleManager.Instance.TargetEnemy = null;

        // 할거 다 하고 null처리
        CardHolder.selectedCard = null;

        // 재정렬 후 베지어 곡선 비활성화
        CardHolder.Relocation();
        _bezierCurve.gameObject.SetActive(false);
    }


    protected override void Use()
    {
        base.Use();
        BattleManager.Instance.TargetEnemy.Hit(5);
    }
}
