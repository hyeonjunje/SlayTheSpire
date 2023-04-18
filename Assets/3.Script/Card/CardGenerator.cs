using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardGenerator : MonoBehaviour, IRegisterable
{
    private int generateNumber = 1;

    [SerializeField]
    private Transform _cardParent;
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


    public BaseCard GenerateRandomCard()
    {
        BaseCard result = null;

        int percentage = Random.Range(0, 100);
        int index = 0;
        int sumAmount = 0;
        for(int i = 0; i < cardPercentage.Count; i++)
        {
            sumAmount += cardPercentage[i];
            if(percentage < sumAmount)
            {
                index = i;
                break;
            }
        }

        int cardType = Random.Range(0, 3);

        if(cardType == (int)ECardType.Attack && index == (int)ECardGrade.Common)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Attack && index == (int)ECardGrade.Special)
        {
            result = GetRandomCard(speicalAttackCardData);
        }
        else if (cardType == (int)ECardType.Attack && index == (int)ECardGrade.Unique)
        {
            result = GetRandomCard(uniqueAttackCardData);
        }
        else if (cardType == (int)ECardType.Skill && index == (int)ECardGrade.Common)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Skill && index == (int)ECardGrade.Special)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Skill && index == (int)ECardGrade.Unique)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Power && index == (int)ECardGrade.Common)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Power && index == (int)ECardGrade.Special)
        {
            result = GetRandomCard(commonAttackCardData);
        }
        else if (cardType == (int)ECardType.Power && index == (int)ECardGrade.Unique)
        {
            result = GetRandomCard(commonAttackCardData);
        }

        return result;
    }

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

        BaseCard baseCard = Instantiate(_baseCard, _cardParent);
        baseCard.Init(_cardHolder, cardData, generateNumber, frameSprite, topFrameSprite);
        generateNumber++;

        return baseCard;
    }


    private BaseCard GetRandomCard(List<CardData> cards)
    {
        int index = Random.Range(0, cards.Count);

        return GenerateCard(cards[index].cardName, cards[index].cardGrade, cards[index].cardType);
    }
}
