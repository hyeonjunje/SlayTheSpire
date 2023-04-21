using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EIndent
{
    Weakening,  // 약화
    Weak, // 취약
    Consciousness, // 의식
    Frenzy, // 격분
    Strength, // 힘 얻기
    Roll, // 몸 말기  -> 공벌레
    SporeCloud,  // 포자 구름
    Split,   // 분열
    Size,
}

[CreateAssetMenu()]
public class IndentData : ScriptableObject
{
    public EIndent indent;
    public Sprite indentSprite;
    public string indentName;
    [Multiline(5)]
    public string indentExplanation;
    public bool isTurn;  // 턴이 지날수록 감소하는 indent인지
    public bool isShowTurn;
}
