using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEvent : MonoBehaviour
{
    BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    RoomManager roomManager => ServiceLocator.Instance.GetService<RoomManager>();
    CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    RelicGenerator relicGenerator => ServiceLocator.Instance.GetService<RelicGenerator>();
    Player player => battleManager.Player;

    // 돈 변화
    public void ChangeMoney(int amount)
    {
        player.PlayerStat.Money += amount;
    }

    // 랜덤 강화
    public void RandomEnforce(int count)
    {
        int temp = 0;
        int c = 0;
        while(c != count)
        {
            int cardIndex = Random.Range(0, player.myCards.Count);
            if(!player.myCards[cardIndex].isEnforce)
            {
                player.myCards[cardIndex].Enforce();
                c++;
            }
            if(temp++ > 100)
            {
                break;
            }
        }
    }


    // 유물 얻기
    public void GainRelic()
    {
        RelicData relicData = relicGenerator.GenerateRandomRelicData();
        battleManager.Player.PlayerRelic.AddRelic(relicData.relic);
    }

    // 성직자 회복
    public void ClericCure()
    {
        if (player.PlayerStat.Money < 35)
            return;

        player.PlayerStat.Money -= 35;
        player.PlayerStat.CurrentHp += 18;

        AfterEvent();
    }

    // 성직자 정화
    public void ClericCleanse()
    {
        if (player.PlayerStat.Money < 50)
            return;

        player.PlayerStat.Money -= 50;

        Discard();
    }

    // 최대 체력 올려주기
    public void ChangeMaxHp(int amount)
    {
        player.PlayerStat.MaxHp += amount;
    }

    // 현재체력 올려주기
    public void ChangeHp(int amount)
    {
        player.PlayerStat.CurrentHp += amount;
    }

    // 무작위 카드 얻기
    public void AddRandomCard()
    {
        player.myCards.Add(cardGenerator.GeneratorRandomCard());
    }

    // 카드 제거
    public void Discard()
    {
        InDiscardUI inDiscardUI = GameObject.Find("MainUI").transform.Find("InDiscardEventUI").GetComponent<InDiscardUI>();

        GameManager.UI.ShowUI(inDiscardUI);
        inDiscardUI.onDiscard = null;
        inDiscardUI.onDiscard += AfterEvent;
    }

    // 카드 강화
    public void Enforce()
    {
        InEnforceUI inDiscardUI = GameObject.Find("MainUI").transform.Find("InEnforceEventUI").GetComponent<InEnforceUI>();

        GameManager.UI.ShowUI(inDiscardUI);
        inDiscardUI.onEnforce = null;
        inDiscardUI.onEnforce += AfterEvent;
    }

    public void Proceed()
    {
        roomManager.NextUnknown();
    }

    public void AfterEvent()
    {
        roomManager.AfterUnknown();
    }

    public void AfterEvent2()
    {
        roomManager.AfterUnknown2();
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
