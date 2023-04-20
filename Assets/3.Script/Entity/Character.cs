using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // Indent Á¤º¸
    // [HideInInspector]
    // public bool[] indent = new bool[(int)EIndent.Size];
    public bool[] indent = new bool[(int)EIndent.Size];

    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public abstract void Dead();
    public abstract void Hit(int damage);
    public abstract void Act();
}
