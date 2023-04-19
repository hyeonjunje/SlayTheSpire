using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ECardUsage
{
    Battle,
    Check,
    Gain
}

public class BaseCard : MonoBehaviour
{
    // ���ı���
    public int generateNumber;
    public ECardType cardType;
    public int cost;
    public string cardName;

    // �뵵 ����
    public ECardUsage cardUsage;

    // ��Ŭ�� �Լ�
    public Action onClickAction;

    [SerializeField]
    private CardController _cardController;
    [SerializeField]
    private BaseCardBuilder _baseCardBuilder;

    private CardHolder _cardHolder;
    private CardData _cardData;   // ī�� ������

    public CardController CardController => _cardController;
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // ī�带 ������ �� �� �Լ��� ����
    public void Init(CardHolder cardHolder, CardData cardData, int generateNumber, Sprite frameSprite, Sprite topFrameSprite)
    {
        _cardHolder = cardHolder;
        _cardData = cardData;

        _cardController.Init(cardHolder, _cardData.isBezierCurve, this);
        _baseCardBuilder.Init(cardData, frameSprite, topFrameSprite, this);

        // ���� ������
        this.generateNumber = generateNumber;
        cardType = _cardData.cardType;
        cost = _cardData.cost;
        cardName = _cardData.cardName;
    }

    public void UseCard()
    {
        if (TryUseCard())
        {
            // ī���� ȿ���� ���
            _cardData.useEffect.ForEach(useEffect => useEffect?.Invoke());

            // ī�� ����
            _cardHolder.DiscardCard(this);

            // ī�� ����ĳ��Ʈ Ȱ��ȭ
            _cardController.SetActiveRaycast(true);
        }
    }

    private bool TryUseCard()
    {
        // �ڽ�Ʈ Ȯ��, ����ī�� Ȯ��, �λ�ī�� Ȯ��, ���� Ȯ��
        if (battleManager.Player.PlayerStat.CurrentOrb >= _cardData.cost)
        {
            battleManager.Player.PlayerStat.CurrentOrb -= _cardData.cost;
            return true;
        }
        else
        {
            _cardController.SetActiveRaycast(true);
            return false;
        }
    }
}
