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
    private bool isBossGoable = false;
    private int height = 1;

    private int battle1Index = 0;
    private int battle2Index = 0;
    private int unknownIndex = 0;

    private Coroutine _coBgmFadeInOut;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    public void Init()
    {
        battle1Index = 0;
        battle2Index = 0;
        unknownIndex = 0;

        firstAct1BattleData.ShuffleList();
        secondAct1BattleData.ShuffleList();
        act1UnknownData.ShuffleList();
    }

/*    private void Awake()
    {
        battle1Index = 0;
        battle2Index = 0;
        unknownIndex = 0;

        firstAct1BattleData.ShuffleList();
        secondAct1BattleData.ShuffleList();
        act1UnknownData.ShuffleList();
    }*/

    public void EnterRoom(ERoomType roomType)
    {
        // ���� �Ҹ� ���
        GameManager.Sound.PlaySE(ESE.EnterRoom);

        battleManager.Player.PlayerStat.Height++;
        if(battleManager.Player.PlayerStat.Height >= 15)
            isBossGoable = true;

        // �Ͽ��� ������ �����ְ�
        neow.gameObject.SetActive(false);

        // �÷��̾� �ʱ�ȭ
        battleManager.Player.gameObject.SetActive(false);
        battleManager.Player.PlayerStat.IsBattle(false);

        // UI��� �����ְ� (��, ����)
        act1Scene.ExitUI();

        // ��ȭâ �����ְ�
        GameManager.UI.InitSelectedButton();

        // �ʱ�ȭ
        inUnknownUI.gameObject.SetActive(false);

        switch (roomType)
        {
            case ERoomType.Elite:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.EliteMeet));

                battleManager.Player.gameObject.SetActive(true);
                battleManager.Player.PlayerStat.IsBattle(true);
                OnEnterEliteRoom();
                break;
            case ERoomType.Enemy:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Level1));

                battleManager.Player.gameObject.SetActive(true);
                battleManager.Player.PlayerStat.IsBattle(true);
                OnEnterEnemyRoom();
                break;
            case ERoomType.Merchant:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Merchant));

                battleManager.Player.gameObject.SetActive(true);
                OnEnterMerchantRoom();
                break;
            case ERoomType.Rest:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Rest));

                OnEnterRestRoom();
                break;
            case ERoomType.Treasure:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Level1));

                battleManager.Player.gameObject.SetActive(true);
                OnEnterTreasureRoom();
                break;
            case ERoomType.Unknown:
                if (_coBgmFadeInOut != null)
                    StopCoroutine(_coBgmFadeInOut);
                _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Level1));

                OnEnterUnknownRoom();
                break;
        }
    }

    // �Ϲ� �� �濡 �� ��
    private void OnEnterEnemyRoom()
    {
        // �ʹݿ� ���� ��
        if(_isEarly)
        {
            battleManager.StartBattle(firstAct1BattleData[battle1Index]);
            battle1Index++;
            if (battle1Index == firstAct1BattleData.Count)
                battle1Index = 0;
        }
        // �Ĺݿ� ���� �� ��
        else
        {
            battleManager.StartBattle(secondAct1BattleData[battle2Index]);
            battle2Index++;
            if (battle2Index == secondAct1BattleData.Count)
                battle2Index = 0;
        }
        GameManager.UI.ShowThisUI(inBattleUI);
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
    public void OnEnterBossRoom()
    {
        if(isBossGoable)
        {
            if (_coBgmFadeInOut != null)
                StopCoroutine(_coBgmFadeInOut);
            _coBgmFadeInOut = StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.BossMeet));

            battleManager.Player.PlayerStat.Height++;

            battleManager.Player.gameObject.SetActive(true);
            battleManager.Player.PlayerStat.IsBattle(true);

            GameManager.UI.ShowThisUI(inBattleUI);
            battleManager.StartBattle(bossAct1BattleData[Random.Range(0, bossAct1BattleData.Count)]);
        }
    }

    // ���� �濡 �� ��
    private void OnEnterUnknownRoom()
    {
        if (unknownIndex == act1UnknownData.Count)
            unknownIndex = 0;

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
        inUnknownUI.ShowAfter(act1UnknownData[unknownIndex-1]);
    }

    public void AfterUnknown2()
    {
        inUnknownUI.ShowAfter2(act1UnknownData[unknownIndex-1]);
    }


    public void ClearRoom()
    {
        GameManager.Game.CurrentRoom.ClearRoom();

        GameManager.UI.ShowUI(mapUI);
    }
}
