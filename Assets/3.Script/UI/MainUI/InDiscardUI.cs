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
    private RectTransform content;   // �ȿ� ������ ������ height�� ��������� ��
    [SerializeField]
    private Transform myCardsParent; // ī���� �θ� �� ������Ʈ
    [SerializeField]
    private Transform discardParent; // ���� �� �θ� �� �������P
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

        // ī�� �ʱ�ȭ �۾�
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

        // ī�� ���� �°� ���� ����
        int cardsRow = (_myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);
    }

    public void Init()
    {
        // ��������
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

    // ���Ÿ� ����� ���
    public void DiscardCancel()
    {
        selectedCard.transform.SetParent(myCardsParent);

        discardCardUI.SetActive(false);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one;

        _exitButton.gameObject.SetActive(true);
    }

    // ���Ÿ� Ȯ���� ���
    public void DiscardYes()
    {
        // ī�� �����ְ�
        selectedCard.Discard();

        discardCardUI.SetActive(false);

        // ���ű���
        discardParent.DestroyAllChild();

        isDiscard = true;
        GameManager.UI.PopUI();

        // ���� ����
        disCardImage.sprite = soldOutSprite;
        disCardCost.SetActive(false);
        battleManager.Player.PlayerStat.Money -= initDiscardCost;
    }
}
