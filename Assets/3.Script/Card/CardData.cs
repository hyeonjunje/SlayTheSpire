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
    ī�� ���̵�, �̸�, ���, ī�� Ÿ��, ī�� ����
     */

    public string CardName { get; private set; }
    public int Cost { get; private set; }
    public string CardExplanation { get; private set; }
    public List<UnityEvent> UseEffect { get; private set; }

    [Header("ī�� ����")]
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
    public bool isExtinction;  // true�� �Ҹ�, false�� �Ҹ� �� �� 
    public ECardFrameData cardFrameData;

    [Header("���ȿ��")]
    public List<UnityEvent> useEffect;

    [Space(3)]
    [Header("��ȭ ī�� ����")]
    public string enforcedCardName;
    public int enforcedCardCost;
    public string enforcedCardExplanation;


    [Header("��ȭ ��� ȿ��")]
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
