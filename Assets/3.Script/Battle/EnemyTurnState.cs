using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BaseBattleState
{
    public EnemyTurnState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.EnemyTurn;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _stateFactory.ChangeState(EBattleState.EnemyTurnEnd);
    }
}
