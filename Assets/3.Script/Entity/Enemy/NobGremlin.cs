using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobGremlin : Enemy
{
    [SerializeField]
    private IndentData indentData;

    public override void Hit(int damage, Character attacker)
    {
        base.Hit(damage, attacker);

        if(indent[(int)EIndent.Frenzy] == true)
        {
            CharacterIndent.AddIndent(indentData, 2);
            CharacterStat.Power += 2;
        }
    }
}
