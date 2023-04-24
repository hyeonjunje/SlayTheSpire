using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCardBuilder : MonoBehaviour
{
    [SerializeField]
    private Image background, frame, topFrame, energy;

    [SerializeField]
    private Text cardName, cardType, cardExplanation, cardCost;
    [SerializeField]
    private Image cardImage;

    private BaseCard _baseCard;

    public void Init(CardData cardData, CardFrameData cardFrameData, BaseCard baseCard)
    {
        // baseī�� ����
        _baseCard = baseCard;

        // ī�� ����
        cardName.text = cardData.cardName;
        cardType.text = cardData.cardTypeString;
        cardExplanation.text = cardData.cardExplanation;
        cardCost.text = cardData.cost.ToString();
        cardImage.sprite = cardData.cardImage;

        // ī�� ������ ����
        background.sprite = cardFrameData.background;
        frame.sprite = cardFrameData.frame;
        topFrame.sprite = cardFrameData.frameTop;
        energy.sprite = cardFrameData.energy;

        // ��ġ ����
        transform.localScale = Vector3.zero;
    }

    public void Enforce(CardData cardData)
    {
        // ���ĵ����� �ֽ�ȭ
        _baseCard.cost = cardData.enforcedCardCost;
        _baseCard.cardName = cardData.enforcedCardName;

        // ī�� ���� �ֽ�ȭ
        cardName.text = cardData.enforcedCardName;
        cardExplanation.text = cardData.enforcedCardExplanation;
        cardCost.text = cardData.enforcedCardCost.ToString();
    }
}
