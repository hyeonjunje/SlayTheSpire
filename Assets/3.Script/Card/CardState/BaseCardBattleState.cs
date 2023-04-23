using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCardBattleState : BaseCardState
{
    private bool _isDrag = false;

    public BaseCardBattleState(BaseCard baseCard, BaseCardStateFactory stateFactory) : base(baseCard, stateFactory)
    {
        cardUsage = ECardUsage.Battle;
    }

    public override void Enter()
    {
        _isDrag = false;
    }

    public override void Exit()
    {

    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;

        _baseCard.CardHolder.selectedCard = _baseCard;

        _baseCard.CardController.StopAllCoroutine();

        _baseCard.transform.SetAsLastSibling();

        if (_baseCard.CardController.IsBezierCurve)
        {
            _baseCard.CardHolder.BezierCurve.gameObject.SetActive(true);
            _baseCard.CardHolder.MoveCenter(_baseCard);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_baseCard.CardController.IsBezierCurve)
        {
            _baseCard.CardHolder.BezierCurve.p0.position = _baseCard.transform.position;
            _baseCard.CardHolder.BezierCurve.p2.position = eventData.position;
        }
        else
        {
            _baseCard.transform.position = eventData.position;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;

        if (_baseCard.CardController.IsBezierCurve) // 타겟이 있으면
        {
            // 타겟이 있을 때만 사용
            if (battleManager.TargetEnemy != null)
            {
                _baseCard.CardController.SetActiveRaycast(false);
                _baseCard.UseCard();
            }

            // 때리고 나면 적 null처리
            battleManager.TargetEnemy = null;

            _baseCard.CardHolder.BezierCurve.gameObject.SetActive(false);
        }
        else
        {
            // 사용가능(코스트 등등)이거나 사용범위에 있을 때만 사용
            // 사용 범위 y값이 300이상 -> 이는 해상도에 따라 바꾸는 로직이 필요
            if (eventData.position.y > 300f)
            {
                _baseCard.CardController.SetActiveRaycast(false);
                _baseCard.UseCard();
            }
        }

        // 할거 다 하고 null처리
        _baseCard.CardHolder.selectedCard = null;
        // 재정렬 후 베지어 곡선 비활성화
        _baseCard.CardHolder.Relocation();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _baseCard.CardHolder.OverCard(_baseCard);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _baseCard.CardHolder.Relocation();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _baseCard.onClickAction?.Invoke();
    }
}
