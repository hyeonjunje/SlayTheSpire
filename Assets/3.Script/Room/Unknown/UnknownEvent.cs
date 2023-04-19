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

    // 돈,
    // 카드 획득, 제거, 변환, 강화
    // 확률적 유물 얻기, 그냥 유물 얻기
    // 최대체력 증가, 체력,
    // 아무일도 없음
}
