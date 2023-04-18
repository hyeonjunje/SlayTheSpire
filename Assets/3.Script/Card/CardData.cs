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

    [Header("���ȿ��")]
    public List<UnityEvent> useEffect;
}
