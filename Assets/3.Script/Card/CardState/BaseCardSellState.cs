using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCardSellState : BaseCardState
{
    public BaseCardSellState(BaseCard baseCard, BaseCardStateFactory stateFactory) : base(baseCard, stateFactory)
    {
        cardUsage = ECardUsage.Sell;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
    }

    public override void OnDrag(PointerEventData eventData)
    {
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _baseCard.onClickAction?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // 상점의 손이 따라오게
        _baseCard.onPointerEnter?.Invoke(_baseCard.transform.position);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }
}
