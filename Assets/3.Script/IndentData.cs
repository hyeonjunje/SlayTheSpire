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
    Size,
}

[CreateAssetMenu()]
public class IndentData : ScriptableObject
{
    public EIndent indent;
    public Sprite indentSprite;
    public bool isTurn;  // ���� �������� �����ϴ� indent����
}
