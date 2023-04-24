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

public class BattleManager : MonoBehaviour, IRegisterable
{
    public System.Action onStartMyTurn;     // 내 턴 시작 시 발생
    public System.Action onEndMyTurn;       // 내 턴 끝 시 발생
    public System.Action onStartEnemyTurn;  // 적 턴 시작 시 발생
    public System.Action onEndEnemyTurn;    // 적 턴 끝 시 발생

    public System.Action onFirstMyTurn;     // 전투 내 첫 턴
    public System.Action onSecondMyTurn;    // 전투 내 두번째 턴

    public System.Action onStartBattle;     // 전투가 시작되면 발생
    public System.Action onEndBattle;        // 전투가 끝나면 발생

    public InBattleUI inBattleUI;
    public InRewardUI inRewardUI;

    public int myTurnCount = 1;
    public bool myTurn = false;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private InGoEndingUI inGoEndingUI;
    [SerializeField]
    private GameScoreUI _gameScoreUI;


    private BattleData _currentBattleData;

    private List<Enemy> _enemies;
    private BattleManagerStateFactory _stateFactory;

    private Coroutine _coBattle = null;

    public Player Player => _player;
    public List<Enemy> Enemies => _enemies;

    private Enemy targetEnemy = null;

    public Enemy TargetEnemy
    {
        get { return targetEnemy; }
        set
        {
            if (Player.cardHolder.selectedCard == null)
                return;

            targetEnemy?.LockOff();
            targetEnemy = value;
            targetEnemy?.LockOn();

            // 타겟이 널이 아니면 베지어 곡선 하이라이트
            Player.cardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }

    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    private void Awake()
    {
        _stateFactory = new BattleManagerStateFactory(this);
    }

    // 버튼에 onclick으로 넣어줌
    public void EndMyTurn()
    {
        myTurn = false;
    }


    public void StartBattle(BattleData battleData)
    {
        // 배틀데이터 저장
        _currentBattleData = battleData;

        // 배틀 UI 활성화
        GameManager.UI.ShowThisUI(inBattleUI);

        // 적 생성
        _enemies = new List<Enemy>();
        for (int i = 0; i < battleData.Enemies.Count; i++)
        {
            Enemy enemy = Object.Instantiate(battleData.Enemies[i], battleData.SpawnPos[i], Quaternion.identity);
            _enemies.Add(enemy);
        }

        myTurnCount = 1;
        myTurn = true;

        onStartBattle?.Invoke();

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

            // 플레이어 죽음 확인
            if (_player.PlayerStat.IsDead)
                break;

            // 적 죽음 확인
            if(_enemies.Count == 0)
                break;

            yield return null;
        }

        if (_player.PlayerStat.IsDead)  // 플레이어가 죽었다.
        {
            _gameScoreUI.GameOver();
            GameManager.UI.ShowThisUI(_gameScoreUI);
        }
        else if(Player.PlayerStat.Height >= 16)  // 보스를 깼다...
        {
            GameManager.UI.ShowThisUI(inGoEndingUI);
        }
        else
        {
            onEndBattle?.Invoke();

            // 클리어 처리
            GameManager.Game.CurrentRoom.ClearRoom();

            // 보상
            Debug.Log("보상을 줍니다.");
            rewardManager.ShowReward(_currentBattleData);
            GameManager.UI.ShowThisUI(inRewardUI);
        }
    }
}
