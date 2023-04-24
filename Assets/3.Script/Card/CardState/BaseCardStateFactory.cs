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

        // ���� ó���� ��Ʋ���·� �ʱ�ȭ
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

        Debug.Log(_currentState + "�� ���°� ��ȯ�Ǿ����ϴ�.");
    }

    public BaseCardState GetState(ECardUsage cardState)
    {
        if (!_dicState.ContainsKey(cardState))
        {
            Debug.Log("�߸��� Ű �Է��Դϴ�.");
            return null;
        }

        return _dicState[cardState];
    }
}
