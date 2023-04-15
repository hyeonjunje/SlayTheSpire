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
        _battleManager.onStartMyTurn?.Invoke();
        _battleManager.Player.onStartTurn?.Invoke();

        _battleManager.myTurn = true;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (_battleManager.inBattleUI.EndStartTurn)
        {
            _stateFactory.ChangeState(EBattleState.MyTurn);
        }
    }
}
