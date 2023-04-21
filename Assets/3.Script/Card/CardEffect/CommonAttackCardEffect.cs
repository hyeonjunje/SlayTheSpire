using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommonAttackCardEffect : BaseCardEffect
{
    [SerializeField]
    private IndentData[] indentData;

    // Ÿ��
    public void Strike()
    {
        targetEnemy.Hit(50 + power, player);
    }

    // ��Ÿ
    public void Bash()
    {
        targetEnemy.Hit(8 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 2);
        targetEnemy.indent[(int)EIndent.Weak] = true;
    }

    // �г�
    public void Anger()
    {
        targetEnemy.Hit(6 + power, player);
    }

    // �����ġ��
    public void BodySlam()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power, player);
    }

    // �ݵ�
    public void Clash()
    {
        targetEnemy.Hit(14 + power, player);
    }

    // ����
    public void Cleave()
    {
        enemies.ForEach(enemy => enemy.Hit(8, player));
    }

    // Ŭ�ν�����
    public void Clothesline()
    {
        targetEnemy.Hit(12 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 2);
        targetEnemy.indent[(int)EIndent.Weakening] = true;
    }

    // ��ġ��
    public void Headbutt()
    {
        targetEnemy.Hit(9 + power, player);
    }

    // ���
    public void HeavyBlade()
    {
        targetEnemy.Hit(14 + power * 3, player);
    }

    // ö�� �ĵ�
    public void IronWave()
    {
        targetEnemy.Hit(5 + power, player);
        player.PlayerStat.Shield += 5 + agility;
    }

    // �Ϻ��� Ÿ��
    public void PerfectedStrike()
    {
        targetEnemy.Hit(6 + power, player);
    }

    // ����Ÿ��
    public void PommelStrike()
    {
        targetEnemy.Hit(9 + power, player);
        player.cardHolder.DrawCard();
    }

    // �θ޶� Į��
    public void SwordBoomerang()
    {
        for(int i = 0; i < 3; i++)
        {
            enemies[Random.Range(0, enemies.Count)].Hit(3 + power, player);
        }
    }

    // õ��
    public void Thunderclap()
    {
        enemies.ForEach(enemy => enemy.Hit(4, player));

        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 1));
        enemies.ForEach(enemy => enemy.indent[(int)EIndent.Weak] = true);
    }

    // ����Ÿ��
    public void TwinStrike()
    {
        targetEnemy.Hit(5 + power, player);
        targetEnemy.Hit(5 + power, player);
    }

    // ������ Ÿ��
    public void WildStrike()
    {
        targetEnemy.Hit(12 + power, player);
    }
}
