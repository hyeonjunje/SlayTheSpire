using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAddCard
{
    Deck,
    Hands,
    Cemetry
}

public class CardEffect : MonoBehaviour
{
    BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // 목표에게 피해를 주기
    public void Hit(int damage)
    {
        battleManager.TargetEnemy.Hit(damage);
    }

    public void Hit(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int count = int.Parse(strs[1]);

        for(int i = 0; i < count; i++)
        {
            battleManager.TargetEnemy.Hit(damage);
        }
    }

    // 방어도 증가
    public void AddShield(int amount)
    {
        battleManager.Player.ShieldAmount += amount;
    }

    // 적 전체 때리기
    public void HitAllEnemy(int damage)
    {
        battleManager.Enemies.ForEach(enemy => enemy.Hit(damage));
    }

    // 취약을 준다.
    public void GiveWeek(int amount)
    {

    }

    // 모든 적에게 취약을 준다.
    public void GiveWeekAllEnemy(int amount)
    {

    }

    // 약화를 준다.
    public void GiveWeakening(int amount)
    {

    }

    // 방어도 만큼 데미지를 준다.
    public void HitShield()
    {
        battleManager.TargetEnemy.Hit(battleManager.Player.ShieldAmount);
    }

    // 플레이어의 힘 효과를 받음
    public void HitPower(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int amount = int.Parse(strs[1]);

        battleManager.TargetEnemy.Hit(damage + amount * 3);
    }

    // '타격' 이 붙은 카드 하나당 피해량 증가
    public void HitStrike(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int amount = int.Parse(strs[1]);
    }

    // 드로우
    public void Draw(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            battleManager.Player.CardHolder.DrawCard();
        }
    }

    // 무작위 적 때림
    public void RandomHit(int damage)
    {
        Enemy randomEnemy = battleManager.Enemies[Random.Range(0, battleManager.Enemies.Count)];
        randomEnemy.Hit(damage);
    }

    // 무작위 적 여러번 때림
    public void RandomHit(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int count = int.Parse(strs[1]);

        for (int i = 0; i < count; i++)
        {
            RandomHit(damage);
        }
    }
    

    // 카드 홀더에 카드 덱에만 카드를 넣어줘야 함
    public void AddCard(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        EAddCard type = (EAddCard)int.Parse(strs[1]);
    }
}
