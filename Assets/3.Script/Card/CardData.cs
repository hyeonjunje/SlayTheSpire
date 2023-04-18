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

[CreateAssetMenu()]
public class CardData : ScriptableObject
{
    /*
    카드 아이디, 이름, 비용, 카드 타입, 카드 설명
     */
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

    [Header("사용효과")]
    public List<UnityEvent> useEffect;
}
