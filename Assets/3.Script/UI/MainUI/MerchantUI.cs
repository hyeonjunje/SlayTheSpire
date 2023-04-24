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

    // 상점 방에 들어올 때마다 값들 갱신
    public void Init()
    {
        // 아이언 클래드 카드 5장 채워넣기
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
        // 무색카드 2장 채워넣기
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

        // 유물 3개 채워넣기
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

        // 지우는 값은 25원씩 비싸짐
        inDiscardUI.initDiscardCost += 25;
        inDiscardUI.isDiscard = false;

        inDiscardUI.Init();
    }

    public void ShowDiscardUI()
    {
        // 이미 팔린 상품이면 핀잔 줘야함
        if(inDiscardUI.isDiscard)
        {
            // 돈이 없다고 핀잔 줘야함

            return;
        }


        // 돈이 있으면
        if(battleManager.Player.PlayerStat.Money >= inDiscardUI.initDiscardCost)
        {
            GameManager.UI.ShowUI(inDiscardUI);
        }
        else
        {
            // 돈이 없다고 핀잔 줘야함
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickIroncladCardItem()
    {
        if(battleManager.Player.PlayerStat.Money >= 75)
        {
            // 가격 지불
            battleManager.Player.PlayerStat.Money -= 75;
            // 새 카드 덱에 넣고 상점에 있는 카드는 없애줌
            BaseCard newCard = cardGenerator.GenerateCard(selectedIronCladCard.CardData.id);
            battleManager.Player.AddCard(newCard);
            Destroy(selectedIronCladCard.gameObject);

            // 샀으면 손 위치 원래대로
            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // 돈이 없다고 핀잔 줘야함
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "이건 자선사업이\n아니에요!";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickNeutralCardItem()
    {
        if(battleManager.Player.PlayerStat.Money >= 100)
        {
            // 가격 지불
            battleManager.Player.PlayerStat.Money -= 100;
            // 새 카드 덱에 넣고 상점에 있는 카드는 없애줌
            BaseCard newCard = cardGenerator.GenerateCard(selectedNeutralCard.CardData.id);
            battleManager.Player.AddCard(newCard);
            Destroy(selectedNeutralCard.gameObject);

            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // 돈이 없다고 핀잔 줘야함
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "당신 빈털털이?";

            GameManager.Sound.PlaySE(ESE.MerchantAngry);
        }
    }

    public void OnClickRelicItem()
    {
        if (battleManager.Player.PlayerStat.Money >= 150)
        {
            // 가격 지불
            battleManager.Player.PlayerStat.Money -= 150;
            // 유물을 생성해서 얻고 상점에 있는 유물을 없애줌
            battleManager.Player.PlayerRelic.AddRelic(selectedRelic.RelicData.relic);
            Destroy(selectedRelic.gameObject);

            StartCoroutine(CoMerchantHandMove(Vector3.up * 1000f));

            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "호호!";

            GameManager.Sound.PlaySE(ESE.MerchantLaugh);
            GameManager.Sound.PlaySE(ESE.BuyItem);
        }
        else
        {
            // 돈이 없다고 핀잔 줘야함
            merchantSpeechBubble.SetActive(true);
            merchantSpeechBubbleText.text = "당신 돈이 없잖아요!";

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
