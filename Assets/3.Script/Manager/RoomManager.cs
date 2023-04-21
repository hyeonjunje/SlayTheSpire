using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private MapUI mapUI;
    [SerializeField]
    private InBattleUI inBattleUI;
    [SerializeField]
    private InUnknownUI inUnknownUI;
    [SerializeField]
    private InMerchantUI inMerchantUI;
    [SerializeField]
    private InRestUI inRestUI;
    [SerializeField]
    private InTreasureUI inTreasureUI;

    [Header("Battle")]
    [SerializeField]
    private List<BattleData> firstAct1BattleData;
    [SerializeField]
    private List<BattleData> secondAct1BattleData;
    [SerializeField]
    private List<BattleData> eliteAct1BattleData;
    [SerializeField]
    private List<BattleData> bossAct1BattleData;
    [Space(3)]

    [Header("Unknown")]
    [SerializeField]
    private List<UnknownData> act1UnknownData;
    [Space(3)]

    [SerializeField]
    private Act1Scene act1Scene;
    [SerializeField]
    private Neow neow;

    private bool _isEarly = true;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    public void EnterRoom(ERoomType roomType)
    {
        // �Ͽ��� ������ �����ְ�
        neow.gameObject.SetActive(false);

        // �÷��̾� ���ֱ�
        battleManager.Player.gameObject.SetActive(true);

        // UI��� �����ְ� (��, ����)
        act1Scene.ExitUI();

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
        GameManager.UI.ShowThisUI(inBattleUI);
        // �ʹݿ� ���� ��
        if(_isEarly)
            battleManager.StartBattle(firstAct1BattleData[Random.Range(0, firstAct1BattleData.Count)]);
        // �Ĺݿ� ���� �� ��
        else
            battleManager.StartBattle(secondAct1BattleData[Random.Range(0, secondAct1BattleData.Count)]);
    }

    // ����Ʈ �濡 �� ��
    private void OnEnterEliteRoom()
    {
        GameManager.UI.ShowThisUI(inBattleUI);
        battleManager.StartBattle(eliteAct1BattleData[Random.Range(0, eliteAct1BattleData.Count)]);
    }

    // ���� �濡 �� ��
    private void OnEnterMerchantRoom()
    {
        GameManager.UI.ShowThisUI(inMerchantUI);
        GameManager.Game.CurrentRoom.ClearRoom();
    }

    // �޽� �濡 �� ��
    private void OnEnterRestRoom()
    {
        battleManager.Player.gameObject.SetActive(false);

        GameManager.UI.ShowThisUI(inRestUI);
        inRestUI.IsUsed = false;
    }

    // ���� �濡 �� ��
    private void OnEnterTreasureRoom()
    {
        GameManager.UI.ShowThisUI(inTreasureUI);
        inTreasureUI.IsUsed = false;
        GameManager.Game.CurrentRoom.ClearRoom();

        _isEarly = false;
    }

    // ���� �濡 �� ��
    private void OnEnterBossRoom()
    {
        GameManager.UI.ShowThisUI(inBattleUI);
        battleManager.StartBattle(bossAct1BattleData[Random.Range(0, bossAct1BattleData.Count)]);
    }

    // ���� �濡 �� ��
    private void OnEnterUnknownRoom()
    {
        GameManager.UI.ShowThisUI(inUnknownUI);
        inUnknownUI.ShowUnknown(act1UnknownData[0]);
    }

    public void ProceedNextUnknown()
    {
        inUnknownUI.ShowNext(act1UnknownData[0]);
    }

    public void ClearRoom()
    {
        GameManager.Game.CurrentRoom.ClearRoom();

        GameManager.UI.ShowUI(mapUI);
    }
}
