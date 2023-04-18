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

    // ��ǥ���� ���ظ� �ֱ�
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

    // �� ����
    public void AddShield(int amount)
    {
        battleManager.Player.ShieldAmount += amount;
    }

    // �� ��ü ������
    public void HitAllEnemy(int damage)
    {
        battleManager.Enemies.ForEach(enemy => enemy.Hit(damage));
    }

    // ����� �ش�.
    public void GiveWeek(int amount)
    {

    }

    // ��� ������ ����� �ش�.
    public void GiveWeekAllEnemy(int amount)
    {

    }

    // ��ȭ�� �ش�.
    public void GiveWeakening(int amount)
    {

    }

    // �� ��ŭ �������� �ش�.
    public void HitShield()
    {
        battleManager.TargetEnemy.Hit(battleManager.Player.ShieldAmount);
    }

    // �÷��̾��� �� ȿ���� ����
    public void HitPower(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int amount = int.Parse(strs[1]);

        battleManager.TargetEnemy.Hit(damage + amount * 3);
    }

    // 'Ÿ��' �� ���� ī�� �ϳ��� ���ط� ����
    public void HitStrike(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        int amount = int.Parse(strs[1]);
    }

    // ��ο�
    public void Draw(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            battleManager.Player.CardHolder.DrawCard();
        }
    }

    // ������ �� ����
    public void RandomHit(int damage)
    {
        Enemy randomEnemy = battleManager.Enemies[Random.Range(0, battleManager.Enemies.Count)];
        randomEnemy.Hit(damage);
    }

    // ������ �� ������ ����
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
    

    // ī�� Ȧ���� ī�� ������ ī�带 �־���� ��
    public void AddCard(string str)
    {
        string[] strs = str.Split(',');
        int damage = int.Parse(strs[0]);
        EAddCard type = (EAddCard)int.Parse(strs[1]);
    }
}
