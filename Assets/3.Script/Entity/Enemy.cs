using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Character, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _reticle;

    protected override void Awake()
    {
        base.Awake();

        onStartTurn += InitShield;
    }

    public override void Dead()
    {
        base.Dead();
    }

    public void DestroyMySelf()
    {
        Destroy(gameObject);
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);
    }

    public override void Act()
    {
        StartCoroutine(CoAct(false));

        BattleManager.Instance.Player.Hit(5);
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
        BattleManager.Instance.TargetEnemy = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BattleManager.Instance.TargetEnemy = null;
    }

    public void InitShield()
    {
        // if 바리케이트 있으면 이건 안해
        ShieldAmount = 0;
    }
}
