using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnEndState : BaseBattleState
{
    CardHolder cardHolder => _battleManager.Player.cardHolder;

    public MyTurnEndState(BattleManager battleManager, BattleManagerStateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.MyTurnEnd;
    }

    public override void Enter()
    {
        _battleManager.onEndMyTurn?.Invoke();
        cardHolder.DiscardAllCard();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _stateFactory.ChangeState(EBattleState.EnemyTurnStart);
    }
}
