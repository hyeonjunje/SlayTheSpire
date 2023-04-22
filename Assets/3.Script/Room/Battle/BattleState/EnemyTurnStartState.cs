using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnStartState : BaseBattleState
{
    public EnemyTurnStartState(BattleManager battleManager, BattleManagerStateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.EnemyTurnStart;
    }

    public override void Enter()
    {
        _battleManager.onStartEnemyTurn?.Invoke();
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
