using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public delegate void OnDieEvent();
    public OnDieEvent onDieEvent;
    
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void Dead()
    {
        onDieEvent?.Invoke();
        Debug.Log("À¸¾Ó ÁÖ±Ý");
    }

    public override void Hit()
    {
        Debug.Log("À¸¾Ó ¾ÆÆÄ");
    }
}
