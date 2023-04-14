using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnState : BaseBattleState
{
    public MyTurnState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.MyTurn;
    }

    // µå·Î¿ì
    public override void Enter()
    {
        for(int i = 0; i < 5; i++)
        {
            GameManager.Game.CardHolder.DrawCard();
        }
    }

    public override void Exit()
    {
        _battleManager.isDone = false;
    }

    public override void Update()
    {
        if(_battleManager.isDone)
        {
            _stateFactory.ChangeState(EBattleState.MyTurnEnd);
        }
    }
}
