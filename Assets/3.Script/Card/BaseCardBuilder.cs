using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCardBuilder : MonoBehaviour
{
    [SerializeField]
    private Image frame, topFrame;
    [SerializeField]
    private Text cardName, cardType, cardExplanation, cardCost;
    [SerializeField]
    private Image cardImage;

    private BaseCard _baseCard;

    public void Init(CardData cardData, Sprite frameSprite, Sprite topFrameSprite, BaseCard baseCard)
    {
        _baseCard = baseCard;

        cardName.text = cardData.cardName;
        cardType.text = cardData.cardTypeString;
        cardExplanation.text = cardData.cardExplanation;
        cardCost.text = cardData.cost.ToString();
        cardImage.sprite = cardData.cardImage;

        frame.sprite = frameSprite;
        topFrame.sprite = topFrameSprite;

        transform.localScale = Vector3.zero;
    }
}
