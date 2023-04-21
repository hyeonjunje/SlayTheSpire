using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Louse : Enemy
{
    [SerializeField] private Sprite origin, rolled;

    private SpriteRenderer spriteRenerer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenerer = GetComponent<SpriteRenderer>();
    }

    public override void Act()
    {
        base.Act();
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void Hit(int damage, Character attacker)
    {
        base.Hit(damage, attacker);

        if(indent[(int)EIndent.Roll])
        {
            indent[(int)EIndent.Roll] = false;
            spriteRenerer.sprite = rolled;
            CharacterStat.Shield += 10;
            CharacterIndent.RemoveIndent(EIndent.Roll);
        }
    }

    protected override void OnEndBattle()
    {
        base.OnEndBattle();
    }

    protected override void OnEndEnemyTurn()
    {
        base.OnEndEnemyTurn();

        spriteRenerer.sprite = origin;
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
