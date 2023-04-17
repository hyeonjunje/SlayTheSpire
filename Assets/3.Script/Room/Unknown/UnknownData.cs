using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RoomData", menuName = "RoomData/UnknownData")]
public class UnknownData : ScriptableObject
{
    public string roomName;
    public Sprite roomImage;
    [Multiline(7)]
    public string roomContents;

    public string[] optionText;
    public List<UnityEvent> optionEvent;
}
