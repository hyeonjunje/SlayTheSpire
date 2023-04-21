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
            indent[(int)EIndent.Strength] = true;
            CharacterStat.Power += 2;
        }
    }
}
