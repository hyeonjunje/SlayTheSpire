using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEvent : MonoBehaviour
{
    Player player => BattleManager.Instance.Player;

    public void ChangeMoney(int amount)
    {
        player.Money += amount;
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
        player.MaxHp += amount;
    }

    public void ChangeHp(int amount)
    {
        player.CurrentHp += amount;
    }

    public void ClearRoom()
    {
        RoomManager.Instance.ClearRoom();
    }

    // ��,
    // ī�� ȹ��, ����, ��ȯ, ��ȭ
    // Ȯ���� ���� ���, �׳� ���� ���
    // �ִ�ü�� ����, ü��,
    // �ƹ��ϵ� ����
}
