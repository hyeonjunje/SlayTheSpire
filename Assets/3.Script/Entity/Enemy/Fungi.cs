using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungi : Enemy
{
    // ������ ������ ���

    [SerializeField] private IndentData indentData;

    public override void Dead()
    {
        base.Dead();
        battleManager.Player.CharacterIndent.AddIndent(indentData, 2);
        battleManager.Player.indent[(int)EIndent.Weak] = true;
    }
}
