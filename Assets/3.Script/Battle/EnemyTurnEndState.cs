using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnEndState : BaseBattleState
{
    public EnemyTurnEndState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.EnemyTurnEnd;
    }

    public override void Enter()
    {
        _battleManager.Enemies.ForEach(enemy => enemy.onEndTurn?.Invoke());
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _stateFactory.ChangeState(EBattleState.MyTurnStart);
    }
}
