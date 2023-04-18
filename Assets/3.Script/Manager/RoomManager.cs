using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private InUnknownUI inUnknownUI;

    [Header("Battle")]
    [SerializeField]
    private List<BattleData> act1BattleData;
    [Space(3)]

    [Header("Unknown")]
    [SerializeField]
    private List<UnknownData> act1UnknownData;
    [Space(3)]

    [SerializeField]
    private Act1Scene act1Scene;
    [SerializeField]
    private Neow neow;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    public void EnterRoom(ERoomType roomType)
    {
        // �Ͽ��� ������ �����ְ�
        neow.gameObject.SetActive(false);

        // UI��� �����ְ� (��, ����)
        act1Scene.ExitUI();
        rewardManager.HideReward();

        // ��ȭâ �����ְ�
        GameManager.UI.InitSelectedButton();

        // �ʱ�ȭ
        inUnknownUI.gameObject.SetActive(false);

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
        battleManager.StartBattle(act1BattleData[0]);
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
        inUnknownUI.gameObject.SetActive(true);
        inUnknownUI.ShowUnknown(act1UnknownData[0]);
    }

    public void ProceedNextUnknown()
    {
        inUnknownUI.ShowNext(act1UnknownData[0]);
    }

    public void ClearRoom()
    {
        // UI ������
        GameManager.Game.CurrentRoom.ClearRoom();

        GameObject.Find("@Act1Scene").GetComponent<Act1Scene>().ShowMap();
    }
}
