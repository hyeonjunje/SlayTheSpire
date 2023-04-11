using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StageData : ScriptableObject
{
    public Sprite sprite;           // 방 이미지
    public Sprite spriteOutline;    // 방 아웃라인 이미지

    public int percentage;          // 방 나올 확률
    public string roomName;         // 방 이름

    [Multiline(5)]
    public string roomExplanation;  // 방 설명
}
