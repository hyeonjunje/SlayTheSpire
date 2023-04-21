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
    public void Init(CardHolder cardHolder, CardData cardData, CardFrameData cardFrameData, int generateNumber)
    {
        _cardHolder = cardHolder;
        _cardData = cardData;

        _cardController.Init(cardHolder, _cardData.isBezierCurve, this);
        _baseCardBuilder.Init(cardData, cardFrameData, this);

        // ���� ������
        this.generateNumber = generateNumber;
        cardType = _cardData.cardType;
        cost = _cardData.cost;
        cardName = _cardData.cardName;
    }

    public void AddCardTemporary()
    {
        _cardHolder.AddCardTemporary(this);
    }

    public void UseCard()
    {
        if (TryUseCard())
        {
            // ī���� ȿ���� ���
            _cardData.useEffect.ForEach(useEffect => useEffect?.Invoke());


            if(_cardData.isExtinction)
            {
                // �Ҹ� ī��� �Ҹ�
                _cardHolder.Extinction(this);
            }
            else
            {
                // ī�� ����
                _cardHolder.DiscardCard(this);
            }
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
