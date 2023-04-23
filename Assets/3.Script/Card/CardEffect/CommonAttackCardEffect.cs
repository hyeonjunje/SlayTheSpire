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

    public void StrikePlus()
    {
        targetEnemy.Hit(9 + power, player);
    }


    // ��Ÿ
    public void Bash()
    {
        targetEnemy.Hit(8 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 2);
    }

    public void BashPlus()
    {
        targetEnemy.Hit(10 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 3);
    }

    // �г�
    public void Anger()
    {
        targetEnemy.Hit(6 + power, player);
    }

    public void AngerPlus()
    {
        targetEnemy.Hit(8 + power, player);
    }

    // �����ġ��
    public void BodySlam()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power, player);
    }

    public void BodySlamPlus()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power, player);
    }

    // �ݵ�
    public void Clash()
    {
        targetEnemy.Hit(14 + power, player);
    }

    public void ClashPlus()
    {
        targetEnemy.Hit(18 + power, player);
    }

    // ����
    public void Cleave()
    {
        enemies.ForEach(enemy => enemy.Hit(8, player));
    }

    public void CleavePlus()
    {
        enemies.ForEach(enemy => enemy.Hit(11, player));
    }

    // Ŭ�ν�����
    public void Clothesline()
    {
        targetEnemy.Hit(12 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 2);
    }

    public void ClotheslinePlus()
    {
        targetEnemy.Hit(14 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 3);
    }

    // ��ġ��
    public void Headbutt()
    {
        targetEnemy.Hit(9 + power, player);
    }

    public void HeadButtPlus()
    {
        targetEnemy.Hit(12 + power, player);
    }

    // ���
    public void HeavyBlade()
    {
        targetEnemy.Hit(14 + power * 3, player);
    }

    public void HeavyBladePlus()
    {
        targetEnemy.Hit(14 + power * 5, player);
    }

    // ö�� �ĵ�
    public void IronWave()
    {
        targetEnemy.Hit(5 + power, player);
        player.PlayerStat.Shield += (5 + agility);
    }

    public void IronWavePlus()
    {
        targetEnemy.Hit(7 + power, player);
        player.PlayerStat.Shield += (7 + agility);
    }

    // �Ϻ��� Ÿ��
    public void PerfectedStrike()
    {
        targetEnemy.Hit(6 + power, player);
    }

    public void PerfectedStrikePlus()
    {
        targetEnemy.Hit(6 + power, player);
    }

    // ����Ÿ��
    public void PommelStrike()
    {
        targetEnemy.Hit(9 + power, player);
        player.cardHolder.DrawCard();
    }

    public void PommelStrikePlus()
    {
        targetEnemy.Hit(10 + power, player);
        player.cardHolder.DrawCard();
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

    public void SwordBoomerangPlus()
    {
        for (int i = 0; i < 4; i++)
        {
            enemies[Random.Range(0, enemies.Count)].Hit(3 + power, player);
        }
    }

    // õ��
    public void Thunderclap()
    {
        enemies.ForEach(enemy => enemy.Hit(4, player));

        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 1));
    }
    public void ThunderclapPlus()
    {
        enemies.ForEach(enemy => enemy.Hit(7, player));

        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 1));
    }

    // ����Ÿ��
    public void TwinStrike()
    {
        targetEnemy.Hit(5 + power, player);
        targetEnemy.Hit(5 + power, player);
    }
    public void TwinStrikePlus()
    {
        targetEnemy.Hit(7 + power, player);
        targetEnemy.Hit(7 + power, player);
    }

    // ������ Ÿ��
    public void WildStrike()
    {
        targetEnemy.Hit(12 + power, player);
    }
    public void WildStrikePlus()
    {
        targetEnemy.Hit(17 + power, player);
    }
}
