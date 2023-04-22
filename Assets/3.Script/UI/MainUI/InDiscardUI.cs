using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class InDiscardUI : BaseUI
{
    [HideInInspector]
    public bool isDiscard = false;
    [HideInInspector]
    public int initDiscardCost = 25;

    private List<BaseCard> _myCards;
    private BaseCard selectedCard;

    [SerializeField]
    private RectTransform content;   // 안에 내용이 많으면 height를 설정해줘야 함
    [SerializeField]
    private Transform myCardsParent; // 카드의 부모가 될 오브젝트
    [SerializeField]
    private Transform discardParent; // 제거 시 부모가 될 오브제긑
    [SerializeField]
    private GameObject discardCardUI;

    [SerializeField]
    private Image disCardImage;
    [SerializeField]
    private GameObject disCardCost;
    [SerializeReference]
    private Text disCardCostText;
    [SerializeField]
    private Sprite discardSprite, soldOutSprite;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();

        // 카드 초기화 작업
        selectedCard = null;

        _myCards = new List<BaseCard>();
        for(int i = 0; i < battleManager.Player.myCards.Count; i++)
        {
            BaseCard card = battleManager.Player.myCards[i];
            _myCards.Add(card);
            card.ChangeState(ECardUsage.DisCard);
            card.transform.SetParent(myCardsParent);
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.one;
            card.onClickAction = null;
            card.onClickAction += (() => selectedCard = card);
            card.onClickAction += ShowDiscard;
        }

        _myCards = _myCards.OrderBy(x => x.generateNumber).ToList();

        // 카드 수에 맞게 높이 조정
        int cardsRow = (_myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);
    }

    public void Init()
    {
        // 상점관련
        disCardCost.SetActive(true);
        disCardImage.sprite = discardSprite;
        disCardCostText.text = initDiscardCost.ToString();
    }

    public void ShowDiscard()
    {
        discardCardUI.SetActive(true);
        selectedCard.transform.SetParent(discardParent);

        selectedCard.transform.localPosition = Vector3.zero;
        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one * 1.5f;

        _exitButton.gameObject.SetActive(false);
    }

    // 제거를 취소할 경우
    public void DiscardCancel()
    {
        selectedCard.transform.SetParent(myCardsParent);

        discardCardUI.SetActive(false);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one;

        _exitButton.gameObject.SetActive(true);
    }

    // 제거를 확정할 경우
    public void DiscardYes()
    {
        // 카드 지워주고
        selectedCard.Discard();

        discardCardUI.SetActive(false);

        // 제거까지
        discardParent.DestroyAllChild();

        isDiscard = true;
        GameManager.UI.PopUI();

        // 상점 관련
        disCardImage.sprite = soldOutSprite;
        disCardCost.SetActive(false);
        battleManager.Player.PlayerStat.Money -= initDiscardCost;
    }
}
