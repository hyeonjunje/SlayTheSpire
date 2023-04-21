using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommonAttackCardEffect : BaseCardEffect
{
    [SerializeField]
    private IndentData[] indentData;

    // 타격
    public void Strike()
    {
        targetEnemy.Hit(50 + power, player);
    }

    // 강타
    public void Bash()
    {
        targetEnemy.Hit(8 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 2);
        targetEnemy.indent[(int)EIndent.Weak] = true;
    }

    // 분노
    public void Anger()
    {
        targetEnemy.Hit(6 + power, player);
    }

    // 몸통박치기
    public void BodySlam()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power, player);
    }

    // 격돌
    public void Clash()
    {
        targetEnemy.Hit(14 + power, player);
    }

    // 절단
    public void Cleave()
    {
        enemies.ForEach(enemy => enemy.Hit(8, player));
    }

    // 클로스라인
    public void Clothesline()
    {
        targetEnemy.Hit(12 + power, player);

        targetEnemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 2);
        targetEnemy.indent[(int)EIndent.Weakening] = true;
    }

    // 박치기
    public void Headbutt()
    {
        targetEnemy.Hit(9 + power, player);
    }

    // 대검
    public void HeavyBlade()
    {
        targetEnemy.Hit(14 + power * 3, player);
    }

    // 철의 파동
    public void IronWave()
    {
        targetEnemy.Hit(5 + power, player);
        player.PlayerStat.Shield += 5 + agility;
    }

    // 완벽한 타격
    public void PerfectedStrike()
    {
        targetEnemy.Hit(6 + power, player);
    }

    // 폼멜타격
    public void PommelStrike()
    {
        targetEnemy.Hit(9 + power, player);
        player.cardHolder.DrawCard();
    }

    // 부메랑 칼날
    public void SwordBoomerang()
    {
        for(int i = 0; i < 3; i++)
        {
            enemies[Random.Range(0, enemies.Count)].Hit(3 + power, player);
        }
    }

    // 천둥
    public void Thunderclap()
    {
        enemies.ForEach(enemy => enemy.Hit(4, player));

        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 1));
        enemies.ForEach(enemy => enemy.indent[(int)EIndent.Weak] = true);
    }

    // 이중타격
    public void TwinStrike()
    {
        targetEnemy.Hit(5 + power, player);
        targetEnemy.Hit(5 + power, player);
    }

    // 난폭한 타격
    public void WildStrike()
    {
        targetEnemy.Hit(12 + power, player);
    }
}
