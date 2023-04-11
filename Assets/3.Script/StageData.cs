using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StageData : ScriptableObject
{
    public Sprite sprite;           // �� �̹���
    public Sprite spriteOutline;    // �� �ƿ����� �̹���

    public int percentage;          // �� ���� Ȯ��
    public string roomName;         // �� �̸�

    [Multiline(5)]
    public string roomExplanation;  // �� ����
}
