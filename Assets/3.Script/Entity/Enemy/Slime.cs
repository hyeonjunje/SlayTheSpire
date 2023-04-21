using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float spawnPosX = 1.5f;
    [SerializeField] private Enemy splitedSlime1, splitedSlime2;
    [SerializeField] private Pattern unKnownPattern;
    private bool isReadyToSplit = false;


    public override void Act()
    {
        // 분열할 준비가 되면 정해진 행동을 하지 않고 죽어서 분열함.
        if(!isReadyToSplit)
        {
            base.Act();
        }
        else
        {
            Dead();
        }
    }

    public override void Dead()
    {
        base.Dead();

        // 분열
        Vector3 initPos1 = new Vector3(transform.position.x + spawnPosX , - 0.6f, 0f);
        Vector3 initPos2 = new Vector3(transform.position.x - spawnPosX , - 0.6f, 0f);

        Enemy enemy1 = Instantiate(splitedSlime1, initPos1, Quaternion.identity);
        Enemy enemy2 = Instantiate(splitedSlime2, initPos2, Quaternion.identity);

        battleManager.Enemies.Add(enemy1);
        battleManager.Enemies.Add(enemy2);

        enemy1.CharacterStat.MaxHp = CharacterStat.CurrentHp;
        enemy1.CharacterStat.CurrentHp = CharacterStat.CurrentHp;

        enemy2.CharacterStat.MaxHp = CharacterStat.CurrentHp;
        enemy2.CharacterStat.CurrentHp = CharacterStat.CurrentHp;
    }

    public override void Hit(int damage, Character attacker)
    {
        base.Hit(damage, attacker);

        // 반피 이하면 분열 준비
        if(CharacterStat.CurrentHp <= CharacterStat.MaxHp / 2)
        {
            isReadyToSplit = true;
            EnemyPattern.DecidePattern(unKnownPattern);
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEndBattle()
    {
        base.OnEndBattle();
    }

    protected override void OnEndEnemyTurn()
    {
        base.OnEndEnemyTurn();
    }

    protected override void OnEndMyTurn()
    {
        base.OnEndMyTurn();
    }

    protected override void OnStartBattle()
    {
        base.OnStartBattle();
    }

    protected override void OnStartEnemyTurn()
    {
        base.OnStartEnemyTurn();
    }

    protected override void OnStartMyTurn()
    {
        base.OnStartMyTurn();
    }
}
