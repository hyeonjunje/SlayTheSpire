using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
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

    public void EnterRoom(ERoomType roomType)
    {
        // 니오우 있으면 없애주고
        neow.gameObject.SetActive(false);

        // UI뜬거 없애주고 (맵..)
        act1Scene.ExitUI();

        // 대화창 없애주고
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

    // 일반 적 방에 들어갈 때
    private void OnEnterEnemyRoom()
    {
        BattleManager.Instance.StartBattle(act1BattleData[0]);
    }

    // 엘리트 방에 들어갈 때
    private void OnEnterEliteRoom()
    {

    }

    // 상인 방에 들어갈 때
    private void OnEnterMerchantRoom()
    {

    }

    // 휴식 방에 들어갈 때
    private void OnEnterRestRoom()
    {

    }

    // 보물 방에 들어갈 때
    private void OnEnterTreasureRoom()
    {

    }

    // 랜덤 방에 들어갈 때
    private void OnEnterUnknownRoom()
    {
        inUnknownUI.gameObject.SetActive(true);
        inUnknownUI.ShowUnknown(act1UnknownData[0]);
    }

    public void ClearRoom()
    {
        // UI 지워줘
        GameManager.Game.CurrentRoom.ClearRoom();

        inUnknownUI.gameObject.SetActive(false);
    }
}
