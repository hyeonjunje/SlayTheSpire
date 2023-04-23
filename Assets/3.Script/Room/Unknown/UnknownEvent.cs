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

    // �� ��ȭ
    public void ChangeMoney(int amount)
    {
        player.PlayerStat.Money += amount;
    }

    // ���� ��ȭ
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


    // ���� ���
    public void GainRelic()
    {
        RelicData relicData = relicGenerator.GenerateRandomRelicData();
        battleManager.Player.PlayerRelic.AddRelic(relicData.relic);
    }

    // ������ ȸ��
    public void ClericCure()
    {
        if (player.PlayerStat.Money < 35)
            return;

        player.PlayerStat.Money -= 35;
        player.PlayerStat.CurrentHp += 18;

        AfterEvent();
    }

    // ������ ��ȭ
    public void ClericCleanse()
    {
        if (player.PlayerStat.Money < 50)
            return;

        player.PlayerStat.Money -= 50;

        Discard();
    }

    // �ִ� ü�� �÷��ֱ�
    public void ChangeMaxHp(int amount)
    {
        player.PlayerStat.MaxHp += amount;
    }

    // ����ü�� �÷��ֱ�
    public void ChangeHp(int amount)
    {
        player.PlayerStat.CurrentHp += amount;
    }

    // ������ ī�� ���
    public void AddRandomCard()
    {
        player.myCards.Add(cardGenerator.GeneratorRandomCard());
    }

    // ī�� ����
    public void Discard()
    {
        InDiscardUI inDiscardUI = GameObject.Find("MainUI").transform.Find("InDiscardEventUI").GetComponent<InDiscardUI>();

        GameManager.UI.ShowUI(inDiscardUI);
        inDiscardUI.onDiscard = null;
        inDiscardUI.onDiscard += AfterEvent;
    }

    // ī�� ��ȭ
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

    // ��,
    // ī�� ȹ��, ����, ��ȯ, ��ȭ
    // Ȯ���� ���� ���, �׳� ���� ���
    // �ִ�ü�� ����, ü��,
    // �ƹ��ϵ� ����
}
