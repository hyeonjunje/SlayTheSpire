using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttackCardEffect : BaseCardEffect
{
    // Ÿ��
    public void Strike()
    {
        targetEnemy.Hit(6 + power);
    }

    // ��Ÿ
    public void Bash()
    {
        targetEnemy.Hit(8 + power);
    }

    // �г�
    public void Anger()
    {
        targetEnemy.Hit(6 + power);
    }

    // �����ġ��
    public void BodySlam()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power);
    }

    // �ݵ�
    public void Clash()
    {
        targetEnemy.Hit(14 + power);
    }

    // ����
    public void Cleave()
    {
        enemies.ForEach(enemy => enemy.Hit(8));
    }

    // Ŭ�ν�����
    public void Clothesline()
    {
        targetEnemy.Hit(12 + power);
    }

    // ��ġ��
    public void Headbutt()
    {
        targetEnemy.Hit(9 + power);
    }

    // ���
    public void HeavyBlade()
    {
        targetEnemy.Hit(14 + power * 3);
    }

    // ö�� �ĵ�
    public void IronWave()
    {
        targetEnemy.Hit(5 + power);
        player.PlayerStat.Shield += 5 + agility;
    }

    // �Ϻ��� Ÿ��
    public void PerfectedStrike()
    {
        targetEnemy.Hit(6 + power);
    }

    // ����Ÿ��
    public void PommelStrike()
    {
        targetEnemy.Hit(9 + power);
        player.cardHolder.DrawCard();
    }

    // �θ޶� Į��
    public void SwordBoomerang()
    {
        for(int i = 0; i < 3; i++)
        {
            enemies[Random.Range(0, enemies.Count)].Hit(3 + power);
        }
    }

    // õ��
    public void Thunderclap()
    {
        enemies.ForEach(enemy => enemy.Hit(4));
    }

    // ����Ÿ��
    public void TwinStrike()
    {
        targetEnemy.Hit(5 + power);
        targetEnemy.Hit(5 + power);
    }

    // ������ Ÿ��
    public void WildStrike()
    {
        targetEnemy.Hit(12 + power);
    }
}
