using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// �� UI�� ī�带 �����ִ� UI�Դϴ�.
/// ���� ī���� �뵵�� ��ȭ��� ��ȭ�� ���ְ�
/// ���Ŷ�� ���Ÿ� ���ְ�
/// show�� ���ָ� show�� ���ݴϴ�. => ���� �� ��, �Ҹ�, ���� ����� ������
/// </summary>
public class InEnforceUI : BaseUI
{
    private List<BaseCard> _myCards;
    private System.Action _callback = null;
    private BaseCard selectedCard;

    [SerializeField]
    private InRestUI inRestUI;
    [SerializeField]
    private RectTransform content;   // �ȿ� ������ ������ height�� ��������� ��
    [SerializeField]
    private Transform myCardsParent; // ī���� �θ� �� ������Ʈ
    [SerializeField]
    private Transform enforceParent;  // ��ȭ �� �θ� �� ������Ʈ
    [SerializeField]
    private GameObject enforceCardUI;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();

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
        for (int i = 0; i < battleManager.Player.myCards.Count; i++)
        {
            BaseCard card = battleManager.Player.myCards[i];
            _myCards.Add(card);
            card.ChangeState(ECardUsage.Enforce);
            card.transform.SetParent(myCardsParent);
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.one;
            card.onClickAction = null;
            card.onClickAction += (() => selectedCard = card);
            card.onClickAction += ShowEnforce;
        }

        // ����
        _myCards = _myCards.OrderBy(x => x.generateNumber).ToList();

        // ī�� ���� �°� ���� ����
        int cardsRow = (_myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);
    }


    private void ShowEnforce()
    {
        enforceCardUI.SetActive(true);
        selectedCard.transform.SetParent(enforceParent);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one * 1.5f;

        BaseCard enforcedCard = cardGenerator.GenerateCard(selectedCard.cardName);
        enforcedCard.Enforce();
        enforcedCard.transform.SetParent(enforceParent);
        enforcedCard.ChangeState(ECardUsage.Enforce);

        enforcedCard.transform.localEulerAngles = Vector3.zero;
        enforcedCard.transform.localScale = Vector3.one * 1.5f;

        _exitButton.gameObject.SetActive(false);
    }

    // ��ȭ�� ����� ���
    public void EnforceCancel()
    {
        selectedCard.transform.SetParent(myCardsParent);
        enforceParent.DestroyAllChild();

        enforceCardUI.SetActive(false);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one;

        _exitButton.gameObject.SetActive(true);
    }

    // ��ȭ�� Ȯ���� ���
    public void EnforceYes()
    {
        selectedCard.Enforce();
        selectedCard.transform.SetParent(myCardsParent);
        enforceParent.DestroyAllChild();

        enforceCardUI.SetActive(false);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one;

        inRestUI.IsUsed = true;

        GameManager.UI.PopUI();
    }
}
