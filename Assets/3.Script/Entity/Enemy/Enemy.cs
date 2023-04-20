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

    // 전투가 시작되면 실행될 함수
    protected virtual void OnStartBattle()
    {
        CharacterIndent.Visualize();
    }

    // 전투가 끝나면 실행될 함수 (근데 어짜피 적들이 죽으면 전투가 끝나기 때문에 일단 만들어만 놓음)
    protected virtual void OnEndBattle()
    {

    }

    // 적 턴이 시작되면 실행될 함수
    protected virtual void OnStartEnemyTurn()
    {
        CharacterStat.Shield = 0;
    }

    protected virtual void OnEndEnemyTurn()
    {
        CharacterIndent.UpdateIndents();
        // 의식이면 내 턴이 시작될 때 파워가 3 상승
        if (indent[(int)EIndent.Consciousness])
        {
            CharacterStat.Power += 3;
        }
    }

    protected virtual void OnEndMyTurn()
    {

    }

    // 내 턴이 시작되면 실행될 함수
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
        Debug.Log("주겄당");
        CharacterAnimation.SetTrigger("isDead");
    }

    public override void Hit(int damage)
    {
        Debug.Log("맞았당");
        CharacterStat.Hit(damage);

        if (!CharacterStat.IsDead)
            CharacterAnimation.SetTrigger("isHitted");
    }

    public override void Act()
    {
        Debug.Log("행동한당");

        EnemyPattern.Act();
        StartCoroutine(CharacterAnimation.CoAct(false));
        CharacterIndent.Visualize();
    }
}
