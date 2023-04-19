using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // 몬스터, 플레이어의 부모 스크립트

    // 자신의 턴이 시작될 때 실행될 델리게이트
    public delegate void OnStartTurn();
    public OnStartTurn onStartTurn;

    // 자신의 턴이 끝날 때 실행될 델리게이트
    public delegate void OnEndTurn();
    public OnEndTurn onEndTurn;

    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public abstract void Dead();
    public abstract void Hit(int damage);
    public abstract void Act();
}
