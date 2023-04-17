using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBattleState
{
    protected BattleManager _battleManager;
    protected StateFactory _stateFactory;
    public EBattleState battleState;

    public BaseBattleState(BattleManager battleManager, StateFactory stateFactory)
    {
        _battleManager = battleManager;
        _stateFactory = stateFactory;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
