using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnStartState : BaseBattleState
{
    public MyTurnStartState(BattleManager battleManager, BattleManagerStateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.MyTurnStart;
    }

    public override void Enter()
    {
        // 내 턴 시작 소리 재생
        GameManager.Sound.PlaySE(ESE.StartMyTun);

        _battleManager.onStartMyTurn?.Invoke();

        if (_battleManager.myTurnCount == 1)
            _battleManager.onFirstMyTurn?.Invoke();
        else if (_battleManager.myTurnCount == 2)
            _battleManager.onSecondMyTurn?.Invoke();

        _battleManager.myTurnCount++;

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
