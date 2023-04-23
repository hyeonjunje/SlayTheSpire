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
        while(indentList.Count != 0)
        {
            Destroy(indentList[0].gameObject);
            indentList.RemoveAt(0);
        }
        indentList = new List<IndentObject>();
    }

    public void AddIndent(IndentData indentData, int value)
    {
        _character.indent[(int)indentData.indent] = true;

        // 이미 있는 Indent라면 turn만 증가
        for(int i = 0; i < indentList.Count; i++)
        {
            if (indentList[i].indentData == indentData)
            {
                indentList[i].AddTurn(value);
                Visualize();
                return;
            }
        }

        indentList.Add(Instantiate(indentPrefab, indentParent));
        indentList[indentList.Count - 1].Init(indentData, value);

        Visualize();
    }

    // 시각화 => indent를 얻을 때나 턴이 시작되면 실행해줌
    public void Visualize()
    {
        for(int i = 0; i < indentList.Count; i++)
        {
            indentList[i].UpdateIndent();
        }
    }

    public void UpdateIndents()
    {
        // 0이 되면 그 indent destroy와
        // indent 배열 false

        List<IndentObject> list = new List<IndentObject>();

        for(int i = 0; i < indentList.Count; i++)
        {
            if(indentList[i].indentData.isTurn)
                indentList[i].turn--;
        }

        for(int i = 0; i < indentList.Count; i++)
        {
            if(indentList[i].indentData.isTurn && indentList[i].turn <= 0)
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
