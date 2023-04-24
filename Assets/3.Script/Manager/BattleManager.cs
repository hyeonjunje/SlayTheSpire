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
    public System.Action onStartMyTurn;     // �� �� ���� �� �߻�
    public System.Action onEndMyTurn;       // �� �� �� �� �߻�
    public System.Action onStartEnemyTurn;  // �� �� ���� �� �߻�
    public System.Action onEndEnemyTurn;    // �� �� �� �� �߻�

    public System.Action onFirstMyTurn;     // ���� �� ù ��
    public System.Action onSecondMyTurn;    // ���� �� �ι�° ��

    public System.Action onStartBattle;     // ������ ���۵Ǹ� �߻�
    public System.Action onEndBattle;        // ������ ������ �߻�

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

            // Ÿ���� ���� �ƴϸ� ������ � ���̶���Ʈ
            Player.cardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }

    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    private void Awake()
    {
        _stateFactory = new BattleManagerStateFactory(this);
    }

    // ��ư�� onclick���� �־���
    public void EndMyTurn()
    {
        myTurn = false;
    }


    public void StartBattle(BattleData battleData)
    {
        // ��Ʋ������ ����
        _currentBattleData = battleData;

        // ��Ʋ UI Ȱ��ȭ
        GameManager.UI.ShowThisUI(inBattleUI);

        // �� ����
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

            // �÷��̾� ���� Ȯ��
            if (_player.PlayerStat.IsDead)
                break;

            // �� ���� Ȯ��
            if(_enemies.Count == 0)
                break;

            yield return null;
        }

        if (_player.PlayerStat.IsDead)  // �÷��̾ �׾���.
        {
            _gameScoreUI.GameOver();
            GameManager.UI.ShowThisUI(_gameScoreUI);
        }
        else if(Player.PlayerStat.Height >= 16)  // ������ ����...
        {
            GameManager.UI.ShowThisUI(inGoEndingUI);
        }
        else
        {
            onEndBattle?.Invoke();

            // Ŭ���� ó��
            GameManager.Game.CurrentRoom.ClearRoom();

            // ����
            Debug.Log("������ �ݴϴ�.");
            rewardManager.ShowReward(_currentBattleData);
            GameManager.UI.ShowThisUI(inRewardUI);
        }
    }
}
