using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnStartState : BaseBattleState
{
    public EnemyTurnStartState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.EnemyTurnStart;
    }

    public override void Enter()
    {
        _battleManager.onStartEnemyTurn?.Invoke();
        _battleManager.Enemies.ForEach(enemy => enemy.onStartTurn?.Invoke());
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (_battleManager.inBattleUI.EndStartTurn)
        {
            _stateFactory.ChangeState(EBattleState.EnemyTurn);
        }
    }
}
