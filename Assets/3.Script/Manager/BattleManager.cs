using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBattleState
{
    MyTurnStart,
    MyTurn,
    MyTurnEnd,
    EnemyTurnStart,
    EnemyTurn,
    EnemyTurnEnd,
    BattleEnd,
}

public class BattleManager : Singleton<BattleManager>
{
    public BattleTurnUI turnUI;
    public TurnEndUI turnEndUI;
    public int myTurnCount = 1;
    private bool _myTurn = false;
    public bool MyTurn
    {
        get { return _myTurn; }
        set
        {
            _myTurn = value;

            
            if(_myTurn)
            {
                turnEndUI.ActiveButton();  // myTurn이 true가 되면 버튼 활성화
            }
            else
            {
                turnEndUI.OnClickButtonEvent(); // myTurn이 false가 되면 클릭한 이벤트 실행
            }
        }
    }

    private Player _player;
    private List<Enemy> _enemies;
    private StateFactory _stateFactory;

    private Coroutine _coBattle = null;

    public Player Player => _player;
    public List<Enemy> Enemies => _enemies;

    private Enemy targetEnemy = null;

    public Enemy TargetEnemy
    {
        get { return targetEnemy; }
        set
        {
            if (Player.CardHolder.selectedCard == null)
                return;

            targetEnemy?.LockOff();
            targetEnemy = value;
            targetEnemy?.LockOn();

            // 타겟이 널이 아니면 베지어 곡선 하이라이트
            Player.CardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }

    public void StartBattle(Player player, List<Enemy> enemies)
    {
        _stateFactory = new StateFactory(this);

        _player = player;
        _enemies = enemies;

        myTurnCount = 1;
        _myTurn = true;

        _stateFactory.ChangeState(EBattleState.MyTurnStart);

        if (_coBattle != null)
        {
            StopCoroutine(_coBattle);
        }
        _coBattle = StartCoroutine(CoBattle());
    }

    IEnumerator CoBattle()
    {
        while(true)
        {
            _stateFactory.CurrentState.Update();

            if(_stateFactory.CurrentState.battleState == EBattleState.BattleEnd)
            {
                Debug.Log("끝났습니다.");
                break;
            }

            yield return null;
        }
        // 상태패턴으로??

        // 내 턴 출력 (몇번째 턴인지도 알려줌)

        // 내가 턴 종료할 때까지 내 턴임

        // 적 턴 출력

        // 적은 순차적으로 행동함
    }
}
