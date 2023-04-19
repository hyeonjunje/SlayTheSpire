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


    private void Awake()
    {
        CharacterStat = GetComponent<CharacterStat>();
        CharacterAnimation = GetComponent<CharacterAnimation>();

        CharacterStat.Init(this);
        CharacterAnimation.Init(this);

        onStartTurn += (() => CharacterStat.Shield = 0);
    }

    public void DestroyMySelf()
    {
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
        battleManager.Player.Hit(5);
        StartCoroutine(CharacterAnimation.CoAct(false));
    }
}
