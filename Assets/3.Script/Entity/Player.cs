using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public delegate void OnDieEvent();
    public OnDieEvent onDieEvent;
    

    public override void Dead()
    {
        onDieEvent?.Invoke();
        Debug.Log("���� �ֱ�");
    }

    public override void Hit(int damage)
    {
        Debug.Log("���� ����");
        base.Hit(damage);
    }
}
