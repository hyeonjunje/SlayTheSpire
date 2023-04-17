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
    /// 사용자가 원하는 카드를 반환하는 카드
    /// </summary>
    /// <param name="cardName">찾을 카드의 이름</param>
    /// <param name="cardGrade">찾을 카드의 등급</param>
    /// <param name="cardType">찾을 카드의 타입</param>
    /// <returns>원하는 카드 반환</returns>
    public BaseCard GenerateCard(string cardName, ECardGrade cardGrade, ECardType cardType)
    {
        CardData cardData = null;
        Sprite frameSprite = null, topFrameSprite = null;

        // 일반 공격 카드
        if (cardGrade == ECardGrade.Common && cardType == ECardType.Attack)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Attack];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonAttackCardData.Find(card => card.cardName == cardName);
        }
        // 특별 공격 카드
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Attack)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Attack];
            topFrameSprite = specialTopFrameSprite;
            cardData = speicalAttackCardData.Find(card => card.cardName == cardName);
        }
        // 희귀 공격 카드
        else if (cardGrade == ECardGrade.Unique && cardType == ECardType.Attack)
        {
            frameSprite = uniqueFrameSprites[(int)ECardType.Attack];
            topFrameSprite = uniqueTopFrameSprite;
            cardData = uniqueAttackCardData.Find(card => card.cardName == cardName);
        }
        // 일반 스킬 카드
        else if (cardGrade == ECardGrade.Common && cardType == ECardType.Skill)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Skill];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonSkillCardData.Find(card => card.cardName == cardName);
        }
        // 특별 스킬 카드
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Skill)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Skill];
            topFrameSprite = specialTopFrameSprite;
            cardData = specialSkillCardData.Find(card => card.cardName == cardName);
        }
        // 특별 희귀 카드
        else if (cardGrade == ECardGrade.Unique && cardType == ECardType.Skill)
        {
            frameSprite = uniqueFrameSprites[(int)ECardType.Skill];
            topFrameSprite = uniqueTopFrameSprite;
            cardData = uniqueSkillCardData.Find(card => card.cardName == cardName);
        }
        // 일반 파워 카드
        else if (cardGrade == ECardGrade.Common && cardType == ECardType.Power)
        {
            frameSprite = commonFrameSprites[(int)ECardType.Power];
            topFrameSprite = commonTopFrameSprite;
            cardData = commonPowerCardData.Find(card => card.cardName == cardName);
        }
        // 특별 파워 카드
        else if (cardGrade == ECardGrade.Special && cardType == ECardType.Power)
        {
            frameSprite = specialFrameSprites[(int)ECardType.Power];
            topFrameSprite = specialTopFrameSprite;
            cardData = specialPowerCardData.Find(card => card.cardName == cardName);
        }
        // 회귀 파워 카드 
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
