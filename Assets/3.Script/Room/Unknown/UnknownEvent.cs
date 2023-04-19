using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEvent : MonoBehaviour
{
    BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    RoomManager roomManager => ServiceLocator.Instance.GetService<RoomManager>();

    public void ChangeMoney(int amount)
    {
        battleManager.Player.PlayerStat.Money += amount;
    }

    public void AddCard()
    {

    }

    public void RemoveCard()
    {

    }

    public void ChangeCard()
    {

    }

    public void EnforceCard()
    {

    }

    public void GainRelics()
    {

    }

    public void GainRelicsPercentage()
    {

    }

    public void ChangeMaxHp(int amount)
    {
        battleManager.Player.PlayerStat.MaxHp += amount;
    }

    public void ChangeHp(int amount)
    {
        battleManager.Player.PlayerStat.CurrentHp += amount;
    }

    public void Proceed()
    {
        roomManager.ProceedNextUnknown();
    }

    public void ClearRoom()
    {
        roomManager.ClearRoom();
    }

    // ��,
    // ī�� ȹ��, ����, ��ȯ, ��ȭ
    // Ȯ���� ���� ���, �׳� ���� ���
    // �ִ�ü�� ����, ü��,
    // �ƹ��ϵ� ����
}
