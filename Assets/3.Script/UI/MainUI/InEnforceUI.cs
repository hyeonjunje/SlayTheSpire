using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 이 UI는 카드를 보여주는 UI입니다.
/// 현재 카드의 용도가 강화라면 강화를 해주고
/// 제거라면 제거를 해주고
/// show를 해주면 show를 해줍니다. => 전투 시 덱, 소멸, 묘지 등등을 보여줌
/// </summary>
public class InEnforceUI : BaseUI
{
    private List<BaseCard> _myCards;
    private System.Action _callback = null;
    private BaseCard selectedCard;

    [SerializeField]
    private InRestUI inRestUI;
    [SerializeField]
    private RectTransform content;   // 안에 내용이 많으면 height를 설정해줘야 함
    [SerializeField]
    private Transform myCardsParent; // 카드의 부모가 될 오브젝트
    [SerializeField]
    private Transform enforceParent;  // 강화 시 부모가 될 오브젝트
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

        // 카드 초기화 작업
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

        // 정렬
        _myCards = _myCards.OrderBy(x => x.generateNumber).ToList();

        // 카드 수에 맞게 높이 조정
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

    // 강화를 취소할 경우
    public void EnforceCancel()
    {
        selectedCard.transform.SetParent(myCardsParent);
        enforceParent.DestroyAllChild();

        enforceCardUI.SetActive(false);

        selectedCard.transform.localEulerAngles = Vector3.zero;
        selectedCard.transform.localScale = Vector3.one;

        _exitButton.gameObject.SetActive(true);
    }

    // 강화를 확정할 경우
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
