using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // 목표에게 피해를 주기
    public void Hit(int damage)
    {
        battleManager.TargetEnemy.Hit(damage);
    }


    public void AddShield(int amount)
    {
        battleManager.Player.ShieldAmount += amount;
    }
}
