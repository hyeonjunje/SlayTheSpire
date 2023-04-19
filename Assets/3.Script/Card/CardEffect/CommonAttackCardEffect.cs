using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttackCardEffect : BaseCardEffect
{
    // 타격
    public void Strike()
    {
        targetEnemy.Hit(6 + power);
    }

    // 강타
    public void Bash()
    {
        targetEnemy.Hit(8 + power);
    }

    // 분노
    public void Anger()
    {
        targetEnemy.Hit(6 + power);
    }

    // 몸통박치기
    public void BodySlam()
    {
        targetEnemy.Hit(player.PlayerStat.Shield + power);
    }

    // 격돌
    public void Clash()
    {
        targetEnemy.Hit(14 + power);
    }

    // 절단
    public void Cleave()
    {
        enemies.ForEach(enemy => enemy.Hit(8));
    }

    // 클로스라인
    public void Clothesline()
    {
        targetEnemy.Hit(12 + power);
    }

    // 박치기
    public void Headbutt()
    {
        targetEnemy.Hit(9 + power);
    }

    // 대검
    public void HeavyBlade()
    {
        targetEnemy.Hit(14 + power * 3);
    }

    // 철의 파동
    public void IronWave()
    {
        targetEnemy.Hit(5 + power);
        player.PlayerStat.Shield += 5 + agility;
    }

    // 완벽한 타격
    public void PerfectedStrike()
    {
        targetEnemy.Hit(6 + power);
    }

    // 폼멜타격
    public void PommelStrike()
    {
        targetEnemy.Hit(9 + power);
        player.cardHolder.DrawCard();
    }

    // 부메랑 칼날
    public void SwordBoomerang()
    {
        for(int i = 0; i < 3; i++)
        {
            enemies[Random.Range(0, enemies.Count)].Hit(3 + power);
        }
    }

    // 천둥
    public void Thunderclap()
    {
        enemies.ForEach(enemy => enemy.Hit(4));
    }

    // 이중타격
    public void TwinStrike()
    {
        targetEnemy.Hit(5 + power);
        targetEnemy.Hit(5 + power);
    }

    // 난폭한 타격
    public void WildStrike()
    {
        targetEnemy.Hit(12 + power);
    }
}
