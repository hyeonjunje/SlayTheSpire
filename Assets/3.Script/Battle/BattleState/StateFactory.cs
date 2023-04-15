using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory
{
    private BattleManager _battleManager;
    private Dictionary<EBattleState, BaseBattleState> _dicState = new Dictionary<EBattleState, BaseBattleState>();

    private BaseBattleState _currentState;

    public BaseBattleState CurrentState => _currentState;

    public StateFactory(BattleManager battleManager)
    {
        _battleManager = battleManager;

        _dicState[EBattleState.MyTurnStart] = new MyTurnStartState(_battleManager, this);
        _dicState[EBattleState.MyTurn] = new MyTurnState(_battleManager, this);
        _dicState[EBattleState.MyTurnEnd] = new MyTurnEndState(_battleManager, this);
        _dicState[EBattleState.EnemyTurnStart] = new EnemyTurnStartState(_battleManager, this);
        _dicState[EBattleState.EnemyTurn] = new EnemyTurnState(_battleManager, this);
        _dicState[EBattleState.EnemyTurnEnd] = new EnemyTurnEndState(_battleManager, this);
    }

    public void ChangeState(EBattleState battleState)
    {
        BaseBattleState newState = GetState(battleState);

        if(_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;

        if(_currentState != null)
        {
            _currentState.Enter();
        }

        Debug.Log(_currentState + "로 상태가 전환되었습니다.");
    }

    public BaseBattleState GetState(EBattleState battleState)
    {
        if(!_dicState.ContainsKey(battleState))
        {
            Debug.Log("잘못된 키 입력입니다.");
            return null;
        }

        return _dicState[battleState];
    }
}
