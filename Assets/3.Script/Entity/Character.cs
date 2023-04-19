using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // ����, �÷��̾��� �θ� ��ũ��Ʈ

    // �ڽ��� ���� ���۵� �� ����� ��������Ʈ
    public delegate void OnStartTurn();
    public OnStartTurn onStartTurn;

    // �ڽ��� ���� ���� �� ����� ��������Ʈ
    public delegate void OnEndTurn();
    public OnEndTurn onEndTurn;

    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public abstract void Dead();
    public abstract void Hit(int damage);
    public abstract void Act();
}
