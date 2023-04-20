using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterIndent : MonoBehaviour
{
    private Character _character;

    [SerializeField] private Transform indentParent;
    [SerializeField] private IndentObject indentPrefab;

    private List<IndentObject> indentList = new List<IndentObject>();


    public void Init(Character character)
    {
        _character = character;
    }

    public void ClearIndentList()
    {
        indentList = new List<IndentObject>();
    }

    public void AddIndent(IndentData indentData, int value)
    {
        // �̹� �ִ� Indent��� turn�� ����
        for(int i = 0; i < indentList.Count; i++)
        {
            if (indentList[i].indentData == indentData)
            {
                indentList[i].AddTurn(value);
                return;
            }
        }

        indentList.Add(Instantiate(indentPrefab, indentParent));
        indentList[indentList.Count - 1].Init(indentData, value);

        Visualize();
    }

    // �ð�ȭ => indent�� ���� ���� ���� ���۵Ǹ� ��������
    public void Visualize()
    {
        for(int i = 0; i < indentList.Count; i++)
        {
            indentList[i].UpdateIndent();
        }
    }

    public void UpdateIndents()
    {
        // 0�� �Ǹ� �� indent destroy��
        // indent �迭 false

        List<IndentObject> list = new List<IndentObject>();

        for(int i = 0; i < indentList.Count; i++)
        {
            if(indentList[i].indentData.isTurn)
                indentList[i].turn--;
        }

        for(int i = 0; i < indentList.Count; i++)
        {
            if(indentList[i].turn <= 0)
            {
                _character.indent[(int)indentList[i].indentData.indent] = false;
                list.Add(indentList[i]);
            }
        }

        while(list.Count > 0)
        {
            IndentObject temp = list[0];
            indentList.Remove(temp);
            list.Remove(temp);
            Destroy(temp.gameObject);
        }

        Visualize();
    }

    public void RemoveIndent(EIndent indentType)
    {
        for (int i = 0; i < indentList.Count; i++)
        {
            if (indentList[i].indentData.indent == indentType)
            {
                IndentObject temp = indentList[i];
                indentList.Remove(temp);
                Destroy(temp.gameObject);
            }
        }
    }
}
