using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EIndent
{
    Weakening,  // ��ȭ
    Weak, // ���
    Consciousness, // �ǽ�
    Frenzy, // �ݺ�
    Strength, // �� ���
    Roll, // �� ����  -> ������
    SporeCloud,  // ���� ����
    Split,   // �п�
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
    public bool isTurn;  // ���� �������� �����ϴ� indent����
    public bool isShowTurn;
}
