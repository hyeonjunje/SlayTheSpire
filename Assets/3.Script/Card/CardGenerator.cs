using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardGenerator : MonoBehaviour
{
    private int generateNumber = 1;

    [SerializeField]
    private CardHolder _cardHolder;
    [SerializeField]
    private BaseCard _baseCard;

    [SerializeField]
    private Sprite[] commonFrameSprites;
    [SerializeField]
    private Sprite commonTopFrameSprite;
    [SerializeField]
    private Sprite[] specialFrameSprites;
    [SerializeField]
    private Sprite specialTopFrameSprite;
    [SerializeField]
    private Sprite[] uniqueFrameSprites;
    [SerializeField]
    private Sprite uniqueTopFrameSprite;

    [SerializeField]
    private List<CardData> commonAttackCardData, speicalAttackCardData, uniqueAttackCardData;

    [SerializeField]
    private List<CardData> commonSkillCardData, specialSkillCardData, uniqueSkillCardData;

    [SerializeField]
    private List<CardData> commonPowerCardData, specialPowerCardData, uniquePowerCardData;

    [SerializeField]
    private List<int> cardPercentage;


    /// <summary>
    /// ����ڰ� ���ϴ� ī�带 ��ȯ�ϴ� ī��
    /// </summary>
    /// <param name="cardName">ã�� ī���� �̸�</param>
    /// <param name="cardGrade">ã�� ī���� ���</param>
    /// <param name="cardType">ã�� ī���� Ÿ��</param>
    /// <returns>���ϴ� ī�� ��ȯ</returns>
    public BaseCard GenerateCard(string cardName, ECardGrade cardGrade, ECardType cardType)
    {
        CardData cardData = null;
        Sprite frameSprite = null, topFrameSprite = null;

        // �Ϲ� ���� ī��
        if (cardGrade == ECardGrade.Common && cardType == ECardType.Attack)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Attack];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonAttackCardData.Find(card => card.cardName == cardName);
        }
        // Ư�� ���� ī��
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Attack)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Attack];
            topFrameSprite = specialTopFrameSprite;
            cardData = speicalAttackCardData.Find(card => card.cardName == cardName);
        }
        // ��� ���� ī��
        else if (cardGrade == ECardGrade.Unique && cardType == ECardType.Attack)
        {
            frameSprite = uniqueFrameSprites[(int)ECardType.Attack];
            topFrameSprite = uniqueTopFrameSprite;
            cardData = uniqueAttackCardData.Find(card => card.cardName == cardName);
        }
        // �Ϲ� ��ų ī��
        else if (cardGrade == ECardGrade.Common && cardType == ECardType.Skill)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Skill];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonSkillCardData.Find(card => card.cardName == cardName);
        }
        // Ư�� ��ų ī��
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Skill)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Skill];
            topFrameSprite = specialTopFrameSprite;
            cardData = specialSkillCardData.Find(card => card.cardName == cardName);
        }
        // Ư�� ��� ī��
        else if (cardGrade == ECardGrade.Unique && cardType == ECardType.Skill)
        {
            frameSprite = uniqueFrameSprites[(int)ECardType.Skill];
            topFrameSprite = uniqueTopFrameSprite;
            cardData = uniqueSkillCardData.Find(card => card.cardName == cardName);
        }
        // �Ϲ� �Ŀ� ī��
        else if (cardGrade == ECardGrade.Common && cardType == ECardType.Power)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Power];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonPowerCardData.Find(card => card.cardName == cardName);
        }
        // Ư�� �Ŀ� ī��
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Power)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Power];
            topFrameSprite = specialTopFrameSprite;
            cardData = specialPowerCardData.Find(card => card.cardName == cardName);
        }
        // ȸ�� �Ŀ� ī�� 
        else if (cardGrade == ECardGrade.Unique && cardType == ECardType.Power)
        {
            frameSprite = uniqueFrameSprites[(int)ECardType.Power];
            topFrameSprite = uniqueTopFrameSprite;
            cardData = uniquePowerCardData.Find(card => card.cardName == cardName);
        }

        BaseCard baseCard = Instantiate(_baseCard, transform);
        baseCard.Init(_cardHolder, cardData, generateNumber, frameSprite, topFrameSprite);
        generateNumber++;

        return baseCard;
    }
}
