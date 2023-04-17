using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndState : BaseBattleState
{
    public BattleEndState(BattleManager battleManager, StateFactory stateFactory) : base(battleManager, stateFactory)
    {
        battleState = EBattleState.BattleEnd;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
}
