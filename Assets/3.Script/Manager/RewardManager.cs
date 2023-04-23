using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private BaseUI inRewardUI;

    [SerializeField]
    private GameObject cardRewardGameObject;
    [SerializeField]
    private GameObject rewardScreen;
    [SerializeField]
    private Transform rewardParent;
    [SerializeField]
    private Transform cardRewardParent;

    [SerializeField]
    private Button passRewardButton;

    [SerializeField]
    private Reward rewardPrefab;

    [SerializeField]
    private Sprite cardRewardImage;
    [SerializeField]
    private Sprite moneyRewardImage;
    [SerializeField]
    private Sprite relicsRewardImage;

    private Reward cardReward;

    private RelicGenerator relicGenrator => ServiceLocator.Instance.GetService<RelicGenerator>();
    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();


    // 보물상자
    public void ShowReward()
    {
        // 돈이랑 유물만
        rewardParent.DestroyAllChild();

        // 보상창 켜주기
        GameManager.UI.ShowThisUI(inRewardUI);

        // 돈
        Reward moneyReward = Instantiate(rewardPrefab, rewardParent);
        Button moneyRewardButton = moneyReward.GetComponent<Button>();
        moneyReward.Init(25 + "골드", moneyRewardImage);
        moneyRewardButton.onClick.AddListener(() => GetMoney(25));
        moneyRewardButton.onClick.AddListener(() => Destroy(moneyReward.gameObject));

        // 유물
        Reward relicsReward = Instantiate(rewardPrefab, rewardParent);
        Button relicRewardButton = relicsReward.GetComponent<Button>();
        RelicData relicData = relicGenrator.GenerateRandomRelicData();
        relicsReward.Init(relicData.relicName, relicData.relicImage);
        relicRewardButton.onClick.AddListener(() => battleManager.Player.PlayerRelic.AddRelic(relicData.relic));
        relicRewardButton.onClick.AddListener(() => Destroy(relicsReward.gameObject));
    }

    // 전투
    public void ShowReward(BattleData battleData)
    {
        rewardParent.DestroyAllChild();
        cardRewardParent.DestroyAllChild();

        // 보상창 켜주기
        GameManager.UI.ShowThisUI(inRewardUI);

        // 전투 끝나고 무조건 카드는 주기 때문에 카드는 일단 생성
        GetCard();

        // 돈
        Reward moneyReward = Instantiate(rewardPrefab, rewardParent);
        Button moneyRewardButton = moneyReward.GetComponent<Button>();
        moneyReward.Init(battleData.money + "골드", moneyRewardImage);
        moneyRewardButton.onClick.AddListener(() => GetMoney(battleData.money));
        moneyRewardButton.onClick.AddListener(() => Destroy(moneyReward.gameObject));


        // 랜덤 카드 선택
        cardReward = Instantiate(rewardPrefab, rewardParent);
        Button cardRewardButton = cardReward.GetComponent<Button>();
        cardReward.Init("덱에 카드를 추가", cardRewardImage);
        cardRewardButton.onClick.AddListener(() => cardRewardGameObject.gameObject.SetActive(true));
        cardRewardButton.onClick.AddListener(() => rewardScreen.gameObject.SetActive(false));

        // 포션 만들꺼면 포션도

        // 유물이 있으면 (엘리트라면)
        if (battleData.isRelics)
        {
            Reward relicsReward = Instantiate(rewardPrefab, rewardParent);
            Button relicRewardButton = relicsReward.GetComponent<Button>();
            RelicData relicData = relicGenrator.GenerateRandomRelicData();
            relicsReward.Init(relicData.relicName, relicData.relicImage);
            relicRewardButton.onClick.AddListener(() => battleManager.Player.PlayerRelic.AddRelic(relicData.relic));
            relicRewardButton.onClick.AddListener(() => Destroy(relicsReward.gameObject));
        }
    }

    // 카드 3장 생성
    private void GetCard()
    {
/*        int randomCardId1 = Random.Range(1, 18);
        int randomCardId2 = Random.Range(1, 18);
        int randomCardId3 = Random.Range(1, 18);*/

        BaseCard card1 = cardGenerator.GeneratorRandomCard();
        BaseCard card2 = cardGenerator.GeneratorRandomCard();
        BaseCard card3 = cardGenerator.GeneratorRandomCard();

        card1.ChangeState(ECardUsage.Gain);
        card2.ChangeState(ECardUsage.Gain);
        card3.ChangeState(ECardUsage.Gain);

        card1.onClickAction = null;
        card2.onClickAction = null;
        card3.onClickAction = null;

        card1.onClickAction += (() => OnClickGainCard());
        card2.onClickAction += (() => OnClickGainCard());
        card3.onClickAction += (() => OnClickGainCard());

        card1.transform.SetParent(cardRewardParent);
        card2.transform.SetParent(cardRewardParent);
        card3.transform.SetParent(cardRewardParent);

        card1.transform.localScale = Vector3.one;
        card2.transform.localScale = Vector3.one;
        card3.transform.localScale = Vector3.one;
    }

    // 보상 카드를 눌렀을 때 실행될 함수
    private void OnClickGainCard()
    {
        // 카드를 얻고
        // 카드 보상 창을 닫고
        // 카드 보상을 없애고

        cardRewardGameObject.gameObject.SetActive(false);
        rewardScreen.gameObject.SetActive(true);
        Destroy(cardReward.gameObject);
    }

    private void GetMoney(int value)
    {
        battleManager.Player.PlayerStat.Money += value;
    }

    private void GetRelic()
    {

    }
}
