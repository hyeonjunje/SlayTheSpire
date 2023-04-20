using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IndentObject : MonoBehaviour
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
        Debug.Log("Áõ°¡ ¾å");
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
}
