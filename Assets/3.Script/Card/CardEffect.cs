using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    // 목표에게 피해를 주기
    public void Hit(int damage)
    {
        BattleManager.Instance.TargetEnemy.Hit(damage);
    }


    public void AddShield(int amount)
    {
        BattleManager.Instance.Player.ShieldAmount += amount;
    }
}
