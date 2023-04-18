using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // ��ǥ���� ���ظ� �ֱ�
    public void Hit(int damage)
    {
        battleManager.TargetEnemy.Hit(damage);
    }


    public void AddShield(int amount)
    {
        battleManager.Player.ShieldAmount += amount;
    }
}
