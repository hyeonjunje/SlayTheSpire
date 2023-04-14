using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnStartState : BaseBattleState
{
    private int myTurnCount = 1;

    public MyTurnStartState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.MyTurnStart;
    }

    public override void Enter()
    {
        // �� �� ��� (���° �������� �˷���)
        _battleManager.turnUI.gameObject.SetActive(true);
        _battleManager.turnUI.DisplayBattleTurn(battleState, myTurnCount);
        myTurnCount++;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (!_battleManager.turnUI.gameObject.activeSelf)
        {
            _stateFactory.ChangeState(EBattleState.MyTurn);
        }
    }
}
