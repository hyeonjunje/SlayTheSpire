using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ECardUsage
{
    Battle,   // ��Ʋ
    Check,    // Ȯ��
    Gain,     // ���
    Enforce,  // ��ȭ
    DisCard,  // ����
    Sell,     // �Ǹ�
}

public class BaseCard : MonoBehaviour
{
    // ���ı���
    public int generateNumber;
    public ECardType cardType;
    public int cost;
    public string cardName;

    public bool isEnforce = false;

    // ��Ŭ�� �Լ�
    public Action onClickAction;

    // ���� �� ������ ���� ������� ������� ����
    public System.Action<Vector3> onPointerEnter;

    [SerializeField]
    private CardController _cardController;
    [SerializeField]
    private BaseCardBuilder _baseCardBuilder;

    private BaseCardStateFactory _baseCardStateFactory;
    private CardHolder _cardHolder;
    private CardData _cardData;   // ī�� ������

    public CardData CardData => _cardData;
    public BaseCardState CurrentState => _baseCardStateFactory.CurrentState;
    public CardHolder  CardHolder => _cardHolder;
    public CardController CardController => _cardController;
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // ī�带 ������ �� �� �Լ��� ����
    public void Init(CardHolder cardHolder, CardData cardData, CardFrameData cardFrameData, int generateNumber)
    {
        _baseCardStateFactory = new BaseCardStateFactory(this);

        cardData.Init();

        _cardHolder = cardHolder;
        _cardData = cardData;

        _cardController.Init(_cardData.isBezierCurve, this);
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
            _cardData.UseEffect.ForEach(useEffect => useEffect?.Invoke());


            if(_cardData.isExtinction)
            {
                GameManager.Sound.PlaySE(ESE.CardExhaust);

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

    // ī���� ���¸� �ٲ�
    public void ChangeState(ECardUsage cardUsage)
    {
        _baseCardStateFactory.ChangeState(cardUsage);
    }

    private bool TryUseCard()
    {
        // �ڽ�Ʈ Ȯ��, ����ī�� Ȯ��, �λ�ī�� Ȯ��, ���� Ȯ��
        if (battleManager.Player.PlayerStat.CurrentOrb >= _cardData.Cost)
        {
            battleManager.Player.PlayerStat.CurrentOrb -= _cardData.Cost;
            return true;
        }
        else
        {
            _cardController.SetActiveRaycast(true);
            return false;
        }
    }

    // ��ȭ����� �� ī�带 ������ �� �Լ��� ����Ǿ�� ��
    public void Enforce()
    {
        // ȭ�� ��鸲
        WindowShake.Instance.ShakeWindow();

        // ��ȭ �Ҹ� ���
        GameManager.Sound.PlaySE(ESE.UpgradeCard);

        _cardData.Enforce();

        // ������ �ٲ��� ��
        _baseCardBuilder.Enforce(_cardData);

        isEnforce = true;
    }

    public void Discard()
    {
        // �� ī�忡�� ������
        battleManager.Player.myCards.Remove(this);
    }
}
