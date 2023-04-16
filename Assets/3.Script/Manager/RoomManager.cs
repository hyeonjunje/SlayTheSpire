using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private List<BattleData> act1BattleData;
    [SerializeField]
    private List<BattleData> act2BattleData;
    [SerializeField]
    private List<BattleData> act3BattleData;

    [SerializeField]
    private Act1Scene act1Scene;
    [SerializeField]
    private Neow neow;

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
        BattleManager.Instance.StartBattle(act1BattleData[0]);
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
