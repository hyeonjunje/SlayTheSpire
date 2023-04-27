using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class IndentObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image indentImage;
    [SerializeField] private Text indentText;

    public IndentData indentData { get; private set; }
    public int turn;

    public void Init(IndentData indentData, int turn)
    {
        this.indentData = indentData;
        this.turn = turn;

        indentImage.sprite = indentData.indentSprite;
        UpdateIndent();
    }

    public void AddTurn(int turn)
    {
        Debug.Log("���� ��");
        this.turn += turn;
    }


    public void UpdateIndent()
    {
        if (indentData.isShowTurn)
        {
            indentText.text = turn.ToString();
        }
        else
        {
            indentText.text = "";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �������� ���̰�
        if(eventData.position.x > Screen.width / 2)
        {
            GameManager.UI.ShowTipUI(indentData.indentName, indentData.indentExplanation, eventData.position, ETipPos.Left);
        }
        // ���������� ���̰�
        else
        {
            GameManager.UI.ShowTipUI(indentData.indentName, indentData.indentExplanation, eventData.position, ETipPos.Right);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.UI.HideTipUI();
    }
}
