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

    private int battle1Index = 0;
    private int battle2Index = 0;
    private int unknownIndex = 0;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    private void Awake()
    {
        battle1Index = 0;
        battle2Index = 0;
        unknownIndex = 0;

        firstAct1BattleData.ShuffleList();
        secondAct1BattleData.ShuffleList();
        act1UnknownData.ShuffleList();
    }

    public void EnterRoom(ERoomType roomType)
    {
        // 니오우 있으면 없애주고
        neow.gameObject.SetActive(false);

        // 플레이어 켜주기
        battleManager.Player.gameObject.SetActive(false);

        // UI뜬거 없애주고 (맵, 보상)
        act1Scene.ExitUI();

        // 대화창 없애주고
        GameManager.UI.InitSelectedButton();

        // 초기화
        inUnknownUI.gameObject.SetActive(false);

        switch (roomType)
        {
            case ERoomType.Elite:
                battleManager.Player.gameObject.SetActive(true);
                OnEnterEliteRoom();
                break;
            case ERoomType.Enemy:
                battleManager.Player.gameObject.SetActive(true);
                OnEnterEnemyRoom();
                break;
            case ERoomType.Merchant:
                battleManager.Player.gameObject.SetActive(true);
                OnEnterMerchantRoom();
                break;
            case ERoomType.Rest:
                OnEnterRestRoom();
                break;
            case ERoomType.Treasure:
                battleManager.Player.gameObject.SetActive(true);
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
        // 초반에 쉬운 적
        if(_isEarly)
        {
            battleManager.StartBattle(firstAct1BattleData[battle1Index]);
            battle1Index++;
            if (battle1Index == firstAct1BattleData.Count)
                battle1Index = 0;
        }
        // 후반에 조금 쎈 적
        else
        {
            battleManager.StartBattle(secondAct1BattleData[battle2Index]);
            battle2Index++;
            if (battle2Index == secondAct1BattleData.Count)
                battle2Index = 0;
        }
        GameManager.UI.ShowThisUI(inBattleUI);
    }

    // 엘리트 방에 들어갈 때
    private void OnEnterEliteRoom()
    {
        GameManager.UI.ShowThisUI(inBattleUI);
        battleManager.StartBattle(eliteAct1BattleData[Random.Range(0, eliteAct1BattleData.Count)]);
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
        battleManager.Player.gameObject.SetActive(false);

        GameManager.UI.ShowThisUI(inRestUI);
        inRestUI.IsUsed = false;
    }

    // 보물 방에 들어갈 때
    private void OnEnterTreasureRoom()
    {
        GameManager.UI.ShowThisUI(inTreasureUI);
        inTreasureUI.IsUsed = false;
        GameManager.Game.CurrentRoom.ClearRoom();

        _isEarly = false;
    }

    // 보스 방에 들어갈 때
    private void OnEnterBossRoom()
    {
        GameManager.UI.ShowThisUI(inBattleUI);
        battleManager.StartBattle(bossAct1BattleData[Random.Range(0, bossAct1BattleData.Count)]);
    }

    // 랜덤 방에 들어갈 때
    private void OnEnterUnknownRoom()
    {
        if (unknownIndex == act1UnknownData.Count)
            unknownIndex = 0;

        Debug.Log(unknownIndex);

        GameManager.UI.ShowThisUI(inUnknownUI);
        inUnknownUI.ShowUnknown(act1UnknownData[unknownIndex]);

        unknownIndex++;
    }

    public void NextUnknown()
    {
        inUnknownUI.ShowNext(act1UnknownData[unknownIndex-1]);
    }

    public void AfterUnknown()
    {
        Debug.Log(unknownIndex-1);

        inUnknownUI.ShowAfter(act1UnknownData[unknownIndex-1]);
    }

    public void AfterUnknown2()
    {
        Debug.Log(unknownIndex-1);

        inUnknownUI.ShowAfter2(act1UnknownData[unknownIndex-1]);
    }


    public void ClearRoom()
    {
        GameManager.Game.CurrentRoom.ClearRoom();

        GameManager.UI.ShowUI(mapUI);
    }
}
