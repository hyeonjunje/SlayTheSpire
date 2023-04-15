using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnEndState : BaseBattleState
{
    CardHolder cardHolder => _battleManager.Player.CardHolder;

    public MyTurnEndState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.MyTurnEnd;
    }

    public override void Enter()
    {
        cardHolder.DiscardAllCard();

        _battleManager.Player.onEndTurn?.Invoke();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _stateFactory.ChangeState(EBattleState.EnemyTurnStart);
    }
}
