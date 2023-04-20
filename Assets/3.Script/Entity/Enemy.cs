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


    private void Awake()
    {
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
    }

    // �� ���� ���۵Ǹ� ����� �Լ�
    private void OnStartEnemyTurn()
    {
        CharacterStat.Shield = 0;
    }

    private void OnEndEnemyTurn()
    {
        // �ǽ��̸� �� ���� ���۵� �� �Ŀ��� 3 ���
        if (indent[(int)EIndent.Consciousness])
        {
            CharacterStat.Power += 3;
        }
    }

    private void OnEndMyTurn()
    {
        CharacterIndent.UpdateIndents();
    }

    // �� ���� ���۵Ǹ� ����� �Լ�
    private void OnStartMyTurn()
    {
        EnemyPattern.DicidePattern();
    }

    public void DestroyMySelf()
    {
        battleManager.onStartMyTurn -= OnStartMyTurn;
        battleManager.onEndMyTurn -= OnEndMyTurn;
        battleManager.onStartEnemyTurn -= OnStartEnemyTurn;
        battleManager.onEndEnemyTurn -= OnEndEnemyTurn;

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
    }
}
