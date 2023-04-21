using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ECardType
{
    Attack,
    Skill,
    Power,
    Common,
    Injury,
    Curse,
}

public enum ECardGrade
{
    Common,
    Special,
    Unique
}

public enum ECardFrameData
{
    AbnormalStatus,
    CommonAttack,
    CommonPower,
    CommonSkill,
    SpeicalAttack,
    SpeicalPower,
    SpeicalSkill,
    UniqueAttack,
    UniquePower,
    UniqueSkill
}

[CreateAssetMenu()]
public class CardData : ScriptableObject
{
    /*
    카드 아이디, 이름, 비용, 카드 타입, 카드 설명
     */

    public string CardName { get; private set; }
    public int Cost { get; private set; }
    public string CardExplanation { get; private set; }
    public List<UnityEvent> UseEffect { get; private set; }

    [Header("카드 정보")]
    public int id;
    public Sprite cardImage;
    public string cardName;
    public int cost;
    public ECardType cardType;
    public string cardTypeString;
    [Multiline(6)]
    public string cardExplanation;
    public ECardGrade cardGrade;
    public bool isBezierCurve;
    public bool isExtinction;  // true면 소멸, false면 소멸 안 함 
    public ECardFrameData cardFrameData;

    [Header("사용효과")]
    public List<UnityEvent> useEffect;

    [Space(3)]
    [Header("강화 카드 정보")]
    public string enforcedCardName;
    public int enforcedCardCost;
    public string enforcedCardExplanation;


    [Header("강화 사용 효과")]
    public List<UnityEvent> enforcedUseEffect;

    public void Init()
    {
        CardName = cardName;
        Cost = cost;
        CardExplanation = cardExplanation;

        UseEffect = new List<UnityEvent>();
        useEffect.ForEach(effect => UseEffect.Add(effect));
    }

    public void Enforce()
    {
        CardName = enforcedCardName;
        Cost = enforcedCardCost;
        CardExplanation = enforcedCardExplanation;

        UseEffect = new List<UnityEvent>();
        useEffect.ForEach(effect => enforcedUseEffect.Add(effect));
    }
}
