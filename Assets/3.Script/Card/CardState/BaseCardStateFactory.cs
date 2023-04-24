using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardStateFactory
{
    private BaseCard _baseCard;
    private Dictionary<ECardUsage, BaseCardState> _dicState = new Dictionary<ECardUsage, BaseCardState>();

    private BaseCardState _currentState;

    public BaseCardState CurrentState => _currentState;

    public BaseCardStateFactory(BaseCard baseCard)
    {
        _baseCard = baseCard;

        _dicState[ECardUsage.Battle] = new BaseCardBattleState(_baseCard, this);
        _dicState[ECardUsage.Check] = new BaseCardCheckState(_baseCard, this);
        _dicState[ECardUsage.Gain] = new BaseCardGainState(_baseCard, this);
        _dicState[ECardUsage.Enforce] = new BaseCardEnforceState(_baseCard, this);
        _dicState[ECardUsage.DisCard] = new BaseCardDiscardState(_baseCard, this);
        _dicState[ECardUsage.Sell] = new BaseCardSellState(_baseCard, this);

        // 가장 처음은 배틀상태로 초기화
        ChangeState(ECardUsage.Battle);
    }

    public void ChangeState(ECardUsage cardState)
    {
        BaseCardState newState = GetState(cardState);

        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.Enter();
        }

        Debug.Log(_currentState + "로 상태가 전환되었습니다.");
    }

    public BaseCardState GetState(ECardUsage cardState)
    {
        if (!_dicState.ContainsKey(cardState))
        {
            Debug.Log("잘못된 키 입력입니다.");
            return null;
        }

        return _dicState[cardState];
    }
}
