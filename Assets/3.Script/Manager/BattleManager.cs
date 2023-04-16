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
    public System.Action onStartMyTurn;     // 내 턴 시작 시 발생
    public System.Action onStartEnemyTurn;  // 적 턴 시작 시 발생

    public InBattleUI inBattleUI;

    public int myTurnCount = 1;
    public bool myTurn = false;

    [SerializeField]
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

    private void Awake()
    {
        _stateFactory = new StateFactory(this);
    }

    // 버튼에 onclick으로 넣어줌
    public void EndMyTurn()
    {
        myTurn = false;
    }


    public void StartBattle(BattleData battleData)
    {
        // 배틀 UI 활성화
        inBattleUI.gameObject.SetActive(true);

        // 플레이어 전투 시작
        _player.StartBattle();

        // 적 생성
        _enemies = new List<Enemy>();
        for (int i = 0; i < battleData.Enemies.Count; i++)
        {
            Enemy enemy = Object.Instantiate(battleData.Enemies[i], battleData.SpawnPos[i], Quaternion.identity);
            _enemies.Add(enemy);
        }

        myTurnCount = 1;
        myTurn = true;

        _stateFactory.ChangeState(EBattleState.MyTurnStart);

        if (_coBattle != null)
        {
            StopCoroutine(_coBattle);
        }
        _coBattle = StartCoroutine(CoBattle());
    }

    IEnumerator CoBattle()
    {
        // isBattle 트루로
        
        while(true)
        {
            _stateFactory.CurrentState.Update();

            // 플레이어 죽음 확인
            if (_player.IsDead)
                break;

            // 적 죽음 확인
            bool isAllEnemyDie = true;

            foreach(Enemy enemy in _enemies)
                if (!enemy.IsDead)
                    isAllEnemyDie = false;

            if (isAllEnemyDie)
                break;


            yield return null;
        }

        // isBattle false로
        _player.EndBattle();

        if (_player.IsDead)
        {

        }
        else
        {
            // 클리어 처리
            GameManager.Game.CurrentRoom.ClearRoom();

            // 보상
        }

        inBattleUI.gameObject.SetActive(false);
    }
}
