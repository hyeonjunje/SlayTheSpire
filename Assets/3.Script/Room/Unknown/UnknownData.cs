using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RoomData", menuName = "RoomData/UnknownData")]
public class UnknownData : ScriptableObject
{
    [Header("이벤트")]
    public string roomName;
    public Sprite roomImage;
    [Multiline(10)]
    public string roomContents;

    public string[] optionText;
    public List<UnityEvent> optionEvent;

    [Space(3)]

    [Header("이벤트 종료")]
    [Multiline(10)]
    public string roomContentsAfter;

    public string[] afterOptionText;
    public List<UnityEvent> afterOptionEvent;
}
