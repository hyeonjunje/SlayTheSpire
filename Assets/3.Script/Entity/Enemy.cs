using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Character, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _reticle;

    public override void Dead()
    {

    }

    public override void Hit()
    {

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
}
