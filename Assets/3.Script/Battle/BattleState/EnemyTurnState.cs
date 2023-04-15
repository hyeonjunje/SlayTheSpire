using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BaseBattleState
{
    float actTime = 1f;
    float currentTime = 0f;
    int enemyIndex = 0;

    public EnemyTurnState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.EnemyTurn;
    }

    public override void Enter()
    {
        currentTime = 0f;
        enemyIndex = 0;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > actTime)
        {
            _battleManager.Enemies[enemyIndex].Act();
            currentTime = 0;
            enemyIndex++;
        }

        // �� �ൿ �� �ϸ� ���� ��ȯ
        if(enemyIndex == _battleManager.Enemies.Count)
        {
            _stateFactory.ChangeState(EBattleState.EnemyTurnEnd);
        }
    }
}
