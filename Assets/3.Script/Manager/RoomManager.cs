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
        // 니오우 있으면 없애주고
        neow.gameObject.SetActive(false);

        // UI뜬거 없애주고 (맵, 보상)
        act1Scene.ExitUI();

        // 대화창 없애주고
        GameManager.UI.InitSelectedButton();

        // 초기화
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

    // 일반 적 방에 들어갈 때
    private void OnEnterEnemyRoom()
    {
        GameManager.UI.ShowThisUI(inBattleUI);
        battleManager.StartBattle(act1BattleData[0]);
    }

    // 엘리트 방에 들어갈 때
    private void OnEnterEliteRoom()
    {

    }

    // 상인 방에 들어갈 때
    private void OnEnterMerchantRoom()
    {
        GameManager.UI.ShowThisUI(inMerchantUI);
        GameManager.Game.CurrentRoom.ClearRoom();
    }

    // 휴식 방에 들어갈 때
    private void OnEnterRestRoom()
    {
        GameManager.UI.ShowThisUI(inRestUI);
        inRestUI.IsUsed = false;
    }

    // 보물 방에 들어갈 때
    private void OnEnterTreasureRoom()
    {
        GameManager.UI.ShowThisUI(inTreasureUI);
        inTreasureUI.IsUsed = false;
        GameManager.Game.CurrentRoom.ClearRoom();
    }

    // 랜덤 방에 들어갈 때
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
