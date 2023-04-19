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

    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();


    public void ShowReward(BattleData battleData)
    {
        rewardParent.DestroyAllChild();
        cardRewardParent.DestroyAllChild();

        // ����â ���ֱ�
        inRewardUI.gameObject.SetActive(true);
        GameManager.UI.ShowThisUI(inRewardUI);

        // ���� ������ ������ ī��� �ֱ� ������ ī��� �ϴ� ����
        GetCard();

        // ��
        Reward moneyReward = Instantiate(rewardPrefab, rewardParent);
        Button moneyRewardButton = moneyReward.GetComponent<Button>();
        moneyReward.Init(battleData.money + "���", moneyRewardImage);
        moneyRewardButton.onClick.AddListener(() => GetMoney(battleData.money));
        moneyRewardButton.onClick.AddListener(() => Destroy(moneyReward.gameObject));


        // ���� ī�� ����
        cardReward = Instantiate(rewardPrefab, rewardParent);
        Button cardRewardButton = cardReward.GetComponent<Button>();
        cardReward.Init("���� ī�带 �߰�", cardRewardImage);
        cardRewardButton.onClick.AddListener(() => cardRewardGameObject.gameObject.SetActive(true));
        cardRewardButton.onClick.AddListener(() => rewardScreen.gameObject.SetActive(false));

        // ���� ���鲨�� ���ǵ�

        // ������ ������ (����Ʈ���)
        if (battleData.isRelics)
        {

        }
    }

    // ī�� 3�� ����
    private void GetCard()
    {
        int randomCardId1 = Random.Range(1, 18);
        int randomCardId2 = Random.Range(1, 18);
        int randomCardId3 = Random.Range(1, 18);

        BaseCard card1 = cardGenerator.GenerateCard(randomCardId1);
        BaseCard card2 = cardGenerator.GenerateCard(randomCardId2);
        BaseCard card3 = cardGenerator.GenerateCard(randomCardId3);

        card1.cardUsage = ECardUsage.Gain;
        card2.cardUsage = ECardUsage.Gain;
        card3.cardUsage = ECardUsage.Gain;

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

    // ���� ī�带 ������ �� ����� �Լ�
    private void OnClickGainCard()
    {
        // ī�带 ���
        // ī�� ���� â�� �ݰ�
        // ī�� ������ ���ְ�

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
