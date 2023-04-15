using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager
{
    private List<BattleData> battleData;

    private Act1Scene act1Scene;
    private Neow neow;

    public void Init()
    {
        battleData = new List<BattleData>();
        battleData.Add(Resources.Load<BattleData>("Data/BattleData/BattleData1"));
    }

    public void SetScene(Act1Scene act1Scene, Neow neow)
    {
        this.act1Scene = act1Scene;
        this.neow = neow;
    }

    public void EnterRoom(ERoomType roomType)
    {
        // �Ͽ��� ������ �����ְ�
        neow.gameObject.SetActive(false);

        // UI��� �����ְ� (��..)
        act1Scene.ExitUI();

        // ��ȭâ �����ְ�
        GameManager.UI.InitSelectedButton();



        switch (roomType)
        {
            case ERoomType.Elite:
                OnEnterEliteRoom();
                break;
            case ERoomType.Enemy:
                OnEnterEnemyRoom();
                break;
            case ERoomType.Merchant:
                OnEnterMerchantRoom();
                break;
            case ERoomType.Rest:
                OnEnterRestRoom();
                break;
            case ERoomType.Treasure:
                OnEnterTreasureRoom();
                break;
            case ERoomType.Unknown:
                OnEnterUnknownRoom();
                break;
        }
    }

    // �Ϲ� �� �濡 �� ��
    private void OnEnterEnemyRoom()
    {
        BattleManager.Instance.StartBattle(battleData[0]);
    }

    // ����Ʈ �濡 �� ��
    private void OnEnterEliteRoom()
    {

    }

    // ���� �濡 �� ��
    private void OnEnterMerchantRoom()
    {

    }

    // �޽� �濡 �� ��
    private void OnEnterRestRoom()
    {

    }

    // ���� �濡 �� ��
    private void OnEnterTreasureRoom()
    {

    }

    // ���� �濡 �� ��
    private void OnEnterUnknownRoom()
    {

    }
}
