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
        _battleManager.turnUI.gameObject.SetActive(true);
        _battleManager.turnUI.DisplayBattleTurn(battleState);

        _battleManager.Enemies.ForEach(enemy => enemy.onStartTurn?.Invoke());
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (!_battleManager.turnUI.gameObject.activeSelf)
        {
            _stateFactory.ChangeState(EBattleState.EnemyTurn);
        }
    }
}
