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
        BattleManager.Instance.StartBattle(battleData[0]);
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

    }
}
