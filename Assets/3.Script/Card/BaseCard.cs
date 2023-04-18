using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BaseCard : MonoBehaviour
{
    // ���ı���
    public int generateNumber;
    public ECardType cardType;
    public int cost;
    public string cardName;

    [SerializeField]
    private CardController _cardController;
    [SerializeField]
    private BaseCardBuilder _baseCardBuilder;

    private CardHolder _cardHolder;
    private CardData _cardData;   // ī�� ������
    private int _generateNumber;  // ���� �ѹ�

    public CardController CardController => _cardController;
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // ī�带 ������ �� �� �Լ��� ����
    public void Init(CardHolder cardHolder, CardData cardData, int generateNumber, Sprite frameSprite, Sprite topFrameSprite)
    {
        _cardHolder = cardHolder;
        _cardData = cardData;
        _generateNumber = generateNumber;

        _cardController.Init(cardHolder, _cardData.isBezierCurve, this);
        _baseCardBuilder.Init(cardData, frameSprite, topFrameSprite, this);

        // ���� ������
        this.generateNumber = generateNumber;
        cardType = _cardData.cardType;
        cost = _cardData.cost;
        cardName = _cardData.cardName;
    }

    // ��Ʋ�� ���۵Ǹ�
    public void StartBattle()
    {
        _cardController.isBattle = true;
    }

    // ��Ʋ�� ������
    public void EndBattle()
    {
        _cardController.isBattle = false;
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
        if (battleManager.Player.Orb >= _cardData.cost)
        {
            battleManager.Player.Orb -= _cardData.cost;
            return true;
        }
        else
        {
            _cardController.SetActiveRaycast(true);
            return false;
        }
    }
}
