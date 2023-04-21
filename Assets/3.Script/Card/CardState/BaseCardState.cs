using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseCardState
{
    protected BaseCard _baseCard;
    protected BaseCardStateFactory _stateFactory;
    public ECardUsage cardUsage;

    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public BaseCardState(BaseCard baseCard, BaseCardStateFactory stateFactory)
    {
        _baseCard = baseCard;
        _stateFactory = stateFactory;
    }

    public abstract void Enter();


    public abstract void OnBeginDrag(PointerEventData eventData);
    public abstract void OnDrag(PointerEventData eventData);
    public abstract void OnEndDrag(PointerEventData eventData);
    public abstract void OnPointerEnter(PointerEventData eventData);
    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);


    public abstract void Exit();
}
