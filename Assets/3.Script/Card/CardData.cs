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
    [Multiline(6)]
    public string enforcedCardExplanation;


    [Header("��ȭ ��� ȿ��")]
    public List<UnityEvent> enforcedUseEffect;
}
