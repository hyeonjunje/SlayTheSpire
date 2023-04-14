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
                turnEndUI.ActiveButton();  // myTurn�� true�� �Ǹ� ��ư Ȱ��ȭ
            }
            else
            {
                turnEndUI.OnClickButtonEvent(); // myTurn�� false�� �Ǹ� Ŭ���� �̺�Ʈ ����
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

            // Ÿ���� ���� �ƴϸ� ������ � ���̶���Ʈ
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
                Debug.Log("�������ϴ�.");
                break;
            }

            yield return null;
        }
        // ������������??

        // �� �� ��� (���° �������� �˷���)

        // ���� �� ������ ������ �� ����

        // �� �� ���

        // ���� ���������� �ൿ��
    }
}
