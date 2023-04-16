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
    public System.Action onStartMyTurn;     // �� �� ���� �� �߻�
    public System.Action onStartEnemyTurn;  // �� �� ���� �� �߻�

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

            // Ÿ���� ���� �ƴϸ� ������ � ���̶���Ʈ
            Player.CardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }

    private void Awake()
    {
        _stateFactory = new StateFactory(this);
    }

    // ��ư�� onclick���� �־���
    public void EndMyTurn()
    {
        myTurn = false;
    }


    public void StartBattle(BattleData battleData)
    {
        // ��Ʋ UI Ȱ��ȭ
        inBattleUI.gameObject.SetActive(true);

        // �÷��̾� ���� ����
        _player.StartBattle();

        // �� ����
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
        // isBattle Ʈ���
        
        while(true)
        {
            _stateFactory.CurrentState.Update();

            // �÷��̾� ���� Ȯ��
            if (_player.IsDead)
                break;

            // �� ���� Ȯ��
            bool isAllEnemyDie = true;

            foreach(Enemy enemy in _enemies)
                if (!enemy.IsDead)
                    isAllEnemyDie = false;

            if (isAllEnemyDie)
                break;


            yield return null;
        }

        // isBattle false��
        _player.EndBattle();

        if (_player.IsDead)
        {

        }
        else
        {
            // Ŭ���� ó��
            GameManager.Game.CurrentRoom.ClearRoom();

            // ����
        }

        inBattleUI.gameObject.SetActive(false);
    }
}
