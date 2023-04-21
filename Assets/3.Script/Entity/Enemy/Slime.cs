using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float spawnPosX = 1.5f;
    [SerializeField] private Enemy splitedSlime;
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
        Enemy enemy1 = Instantiate(splitedSlime, transform.position + Vector3.right * spawnPosX, Quaternion.identity);
        Enemy enemy2 = Instantiate(splitedSlime, transform.position + Vector3.left * spawnPosX, Quaternion.identity);
        battleManager.Enemies.Add(enemy1);
        battleManager.Enemies.Add(enemy2);
        enemy1.CharacterStat.MaxHp = CharacterStat.CurrentHp;
        enemy1.CharacterStat.CurrentHp = CharacterStat.CurrentHp;
        enemy2.CharacterStat.MaxHp = CharacterStat.CurrentHp;
        enemy2.CharacterStat.CurrentHp = CharacterStat.CurrentHp;
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);

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
