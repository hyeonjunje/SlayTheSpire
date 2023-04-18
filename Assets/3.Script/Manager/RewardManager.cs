using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private GameObject rewardUI;

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

    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    private void Awake()
    {
        passRewardButton.onClick.AddListener(() => GameObject.Find("@Act1Scene").GetComponent<Act1Scene>().ShowMap());
    }

    public void ShowReward(BattleData battleData)
    {
        Init();

        // 보상창 켜주기
        rewardUI.gameObject.SetActive(true);

        // 전투 끝나고 무조건 카드는 주기 때문에 카드는 일단 생성
        GetCard();

        // 돈
        Reward moneyReward = Instantiate(rewardPrefab, rewardParent);
        Button moneyRewardButton = moneyReward.GetComponent<Button>();
        moneyReward.Init(battleData.money + "골드", moneyRewardImage);
        moneyRewardButton.onClick.AddListener(() => GetMoney(battleData.money));
        moneyRewardButton.onClick.AddListener(() => Destroy(moneyReward.gameObject));


        // 랜덤 카드 선택
        Reward cardReward = Instantiate(rewardPrefab, rewardParent);
        Button cardRewardButton = cardReward.GetComponent<Button>();
        cardReward.Init("덱에 카드를 추가", cardRewardImage);
        cardRewardButton.onClick.AddListener(() => cardRewardGameObject.gameObject.SetActive(true));
        cardRewardButton.onClick.AddListener(() => rewardScreen.gameObject.SetActive(false));

        // 포션 만들꺼면 포션도

        // 유물이 있으면 (엘리트라면)
        if (battleData.isRelics)
        {

        }
    }

    public void HideReward()
    {
        rewardUI.gameObject.SetActive(false);
    }

    private void Init()
    {
        rewardParent.DestroyAllChild();
        cardRewardParent.DestroyAllChild();

        rewardUI.gameObject.SetActive(false);
    }


    // 카드 3장 생성
    private void GetCard()
    {
        BaseCard card1 = cardGenerator.GenerateCard(1);
        BaseCard card2 = cardGenerator.GenerateCard(1);
        BaseCard card3 = cardGenerator.GenerateCard(1);

        card1.transform.SetParent(cardRewardParent);
        card2.transform.SetParent(cardRewardParent);
        card3.transform.SetParent(cardRewardParent);

        card1.transform.localScale = Vector3.one;
        card2.transform.localScale = Vector3.one;
        card3.transform.localScale = Vector3.one;
    }

    private void GetMoney(int value)
    {
        battleManager.Player.Money += value;
    }

    private void GetRelic()
    {

    }
}
