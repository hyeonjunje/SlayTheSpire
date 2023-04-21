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
    private BaseCard _baseCardPrefab;

    [SerializeField]
    private List<CardFrameData> cardFrameDataArray;
    [SerializeField]
    private List<CardData> allCardList;
    [SerializeField]
    private List<CardData> abnormalStatusCardData;
    [SerializeField]
    private List<int> cardPercentage;

    public BaseCard GenerateCard(int id)
    {
        for(int i = 0; i < allCardList.Count; i++)
        {
            if(allCardList[i].id == id)
            {
                return GenerateCard(allCardList[i].cardName);
            }
        }

        Debug.LogError("그런 카드는 없습니다.");

        return null;
    }

    public BaseCard GeneratorRandomCard()
    {
        CardData randomCardData = allCardList[Random.Range(0, allCardList.Count)];
        return GenerateCard(randomCardData.cardName);
    }

    /// <summary>
    /// 카드의 이름으로 그 카드를 생성해서 가져올 수 있다.
    /// </summary>
    /// <param name="cardName">생성된 카드</param>
    /// <returns></returns>
    private BaseCard GenerateCard(string cardName)
    {
        BaseCard baseCard = Instantiate(_baseCardPrefab, _cardParent);
        CardData cardData = allCardList.Find(card => card.cardName == cardName);
        baseCard.Init(_cardHolder, cardData, cardFrameDataArray[(int)cardData.cardFrameData], generateNumber);
        generateNumber++;

        return baseCard;
    }
}
