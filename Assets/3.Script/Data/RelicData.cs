using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERelic
{
    BurningBlood,
    Vajra,
    BagOfMarbles,
    Anchor,
    Strawberry,
    SmoothStone,
    BagOfPreparation,
    BloodVial,
    RegalPilow,
    Lantern,
    Meat,
    Pear,
    HornCleat,
    Mango,
    Ginger,
    Turnip,
    Torii,
    Size
}

[CreateAssetMenu()]
public class RelicData : ScriptableObject
{
    public ERelic relic;
    public Sprite relicImage;
    public string relicName;
    [Multiline(5)]
    public string relicExplanation;

    public enum ERelicGrade { Common, Special, Unique}
    public ERelicGrade relicGrade;
}
