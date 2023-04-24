using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUI : BaseUI
{
    [SerializeField]
    private float merchantHandSpeed;

    [SerializeField]
    private GameObject merchantSpeechBubble;
    [SerializeField]
    private Text merchantSpeechBubbleText;

    [SerializeField]
    private Transform merchantHandPoint;
    [SerializeField]
    private GameObject discardButtonCostPrefab;
    [SerializeField]
    private RelicIcon relic;
    [SerializeField]
    private InDiscardUI inDiscardUI;
    [SerializeField]
    private Transform ironcladCardItemParent;
    [SerializeField]
    private Transform neutralCardItemParent;
    [SerializeField]
    private Transform relicItemParent;

    private BaseCard selectedIronCladCard;
    private BaseCard selectedNeutralCard;
    private RelicIcon selectedRelic;

    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private RelicGenerator relicGenerator => ServiceLocator.Instance.GetService<RelicGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
    }

    // ���� �濡 ���� ������ ���� ����
    public void Init()
    {
        // ���̾� Ŭ���� ī�� 5�� ä���ֱ�
        for(int i = 0; i < 5; i++)
        {
            BaseCard card = cardGenerator.GeneratorRandomCard();
            card.transform.SetParent(ironcladCardItemParent);
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.one;
            card.ChangeState(ECardUsage.Sell);
            card.onClickAction = null;
            card.onClickAction += (() => selectedIronCladCard = card);
            card.onClickAction += OnClickIroncladCardItem;

            card.onPointerEnter += ((pos) => StartCoroutine(CoMerchantHandMove(pos)));
            card.onPointerEnter += ((pos) => merchantSpeechBubble.SetActive(false));

            GameObject go = Instantiate(discardButtonCostPrefab, card.transform);
            go.GetComponentInChildren<Text>().text = "75";
        }
        // ����ī�� 2�� ä���ֱ�
        for (int i = 0; i < 2; i++)
        {
            BaseCard card = cardGenerator.GeneratorRandomCard();
            card.transform.SetParent(neutralCardItemParent);
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.one;
            card.ChangeState(ECardUsage.Sell);
            card.onClickAction = null;
            card.onClickAction += (() => selectedNeutralCard = card);
            card.onClickAction += OnClickNeutralCardItem;

            card.onPointerEnter += ((pos) => StartCoroutine(CoMerchantHandMove(pos)));
            card.onPointerEnter += ((pos) => merchantSpeechBubble.SetActive(false));

            GameObject go = Instantiate(discardButtonCostPrefab, card.transform);
            go.GetComponentInChildren<Text>().text = "100";
        }

        // ���� 3�� ä���ֱ�
        for(int i = 0; i < 3; i++)
        {
            RelicData relicData = relicGenerator.GenerateRandomRelicData();
            RelicIcon relicObj = Instantiate(relic, relicItemParent);
            relicObj.Init(relicData);
            relicObj.onClickRelic = null;
            relicObj.onClickRelic += (() => selectedRelic = relicObj);
            relicObj.onClickRelic += OnClickRelicItem;

            relicObj.onPointerEnter += ((pos) => StartCoroutine(CoMerchantHandMove(pos)));
            relicObj.onPointerEnter += ((pos) => merchantSpeechBubble.SetActive(false));

            GameObject go = Instantiate(discardButtonCostPrefab, relicObj.transform);
            go.GetComponentInChildren<Text>().text = "150";
        }

        // ����� ���� 25���� �����
        inDiscardUI.initDiscardCost += 25;
        inDiscardUI.isDiscard = false;

        inDiscardUI.Init();
    }

    public void ShowDiscardUI()
    {
        // �̹� �ȸ� ��ǰ�̸� ���� �����
        if(inDiscardUI.isDiscard)
        {
            // ���� ���ٰ� ���� �����

            return;
        }


        // ���� ������
        if(battleManager.Player.PlayerStat.Money >= inDiscardUI.initDiscardCost)
        {
            GameManager.UI.ShowUI(inDiscardUI);
        }
        else
        {
            // ���� ���ٰ� ���� �����
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickIroncladCardItem()
    {
        if(battleManager.Player.PlayerStat.Money >= 75)
        {
            // ���� ����
            battleManager.Player.PlayerStat.Money -= 75;
            // �� ī�� ���� �ְ� ������ �ִ� ī��� ������
            BaseCard newCard = cardGenerator.GenerateCard(selectedIronCladCard.CardData.id);
            battleManager.Player.AddCard(newCard);
            Destroy(selectedIronCladCard.gameObject);

            // ������ �� ��ġ �������
            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // ���� ���ٰ� ���� �����
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "�̰� �ڼ������\n�ƴϿ���!";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickNeutralCardItem()
    {
        if(battleManager.Player.PlayerStat.Money >= 100)
        {
            // ���� ����
            battleManager.Player.PlayerStat.Money -= 100;
            // �� ī�� ���� �ְ� ������ �ִ� ī��� ������
            BaseCard newCard = cardGenerator.GenerateCard(selectedNeutralCard.CardData.id);
            battleManager.Player.AddCard(newCard);
            Destroy(selectedNeutralCard.gameObject);

            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // ���� ���ٰ� ���� �����
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "��� ��������?";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickRelicItem()
    {
        if (battleManager.Player.PlayerStat.Money >= 150)
        {
            // ���� ����
            battleManager.Player.PlayerStat.Money -= 150;
            // ������ �����ؼ� ��� ������ �ִ� ������ ������
            battleManager.Player.PlayerRelic.AddRelic(selectedRelic.RelicData.relic);
            Destroy(selectedRelic.gameObject);

            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "ȣȣ!";

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // ���� ���ٰ� ���� �����
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "��� ���� ���ݾƿ�!";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    private IEnumerator CoMerchantHandMove(Vector3 targetPos)
    {
        float currentTime = 0;
        Vector3 initPos = merchantHandPoint.position;
        while (currentTime < merchantHandSpeed)
        {
            currentTime += Time.deltaTime;

            merchantHandPoint.position = Vector3.Lerp(initPos, targetPos, currentTime / merchantHandSpeed);
            yield return null;
        }
    }
}
