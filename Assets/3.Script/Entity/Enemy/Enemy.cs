using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Character, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _reticle;

    public CharacterStat CharacterStat { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public EnemyPattern EnemyPattern { get; private set; }
    public CharacterIndent CharacterIndent { get; private set; }


    protected virtual void Awake()
    {
        indent = new bool[(int)EIndent.Size];

    CharacterStat = GetComponent<CharacterStat>();
        CharacterAnimation = GetComponent<CharacterAnimation>();
        EnemyPattern = GetComponent<EnemyPattern>();
        CharacterIndent = GetComponent<CharacterIndent>();

        CharacterStat.Init(this);
        CharacterAnimation.Init(this);
        EnemyPattern.Init(this);
        CharacterIndent.Init(this);

        battleManager.onStartMyTurn += OnStartMyTurn;
        battleManager.onEndMyTurn += OnEndMyTurn;
        battleManager.onStartEnemyTurn += OnStartEnemyTurn;
        battleManager.onEndEnemyTurn += OnEndEnemyTurn;

        battleManager.onStartBattle += OnStartBattle;
        battleManager.onEndBattle += OnEndBattle;
    }

    // ������ ���۵Ǹ� ����� �Լ�
    protected virtual void OnStartBattle()
    {
        CharacterIndent.Visualize();
    }

    // ������ ������ ����� �Լ� (�ٵ� ��¥�� ������ ������ ������ ������ ������ �ϴ� ���� ����)
    protected virtual void OnEndBattle()
    {

    }

    // �� ���� ���۵Ǹ� ����� �Լ�
    protected virtual void OnStartEnemyTurn()
    {
        CharacterStat.Shield = 0;
    }

    protected virtual void OnEndEnemyTurn()
    {
        CharacterIndent.UpdateIndents();
        // �ǽ��̸� �� ���� ���۵� �� �Ŀ��� 3 ���
        if (indent[(int)EIndent.Consciousness])
        {
            CharacterStat.Power += 3;
        }
    }

    protected virtual void OnEndMyTurn()
    {

    }

    // �� ���� ���۵Ǹ� ����� �Լ�
    protected virtual void OnStartMyTurn()
    {
        EnemyPattern.DicidePattern();
    }

    public void DestroyMySelf()
    {
        battleManager.onStartMyTurn -= OnStartMyTurn;
        battleManager.onEndMyTurn -= OnEndMyTurn;
        battleManager.onStartEnemyTurn -= OnStartEnemyTurn;
        battleManager.onEndEnemyTurn -= OnEndEnemyTurn;
        battleManager.onStartBattle -= OnStartBattle;
        battleManager.onEndBattle -= OnEndBattle;

        battleManager.Enemies.Remove(this);
        Destroy(gameObject);
    }

    public void LockOn()
    {
        _reticle.SetActive(true);
    }

    public void LockOff()
    {
        _reticle.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        battleManager.TargetEnemy = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        battleManager.TargetEnemy = null;
    }

    public override void Dead()
    {
        Debug.Log("�ְδ�");
        CharacterAnimation.SetTrigger("isDead");
    }

    public override void Hit(int damage)
    {
        Debug.Log("�¾Ҵ�");
        CharacterStat.Hit(damage);

        if (!CharacterStat.IsDead)
            CharacterAnimation.SetTrigger("isHitted");
    }

    public override void Act()
    {
        Debug.Log("�ൿ�Ѵ�");

        EnemyPattern.Act();
        StartCoroutine(CharacterAnimation.CoAct(false));
        CharacterIndent.Visualize();
    }
}
