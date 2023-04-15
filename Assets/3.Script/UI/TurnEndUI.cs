using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurnEndUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image image;
    private string title, contents;

    [SerializeField]
    private Color lockSpriteColor, originSpriteColor;

    [SerializeField]
    private GameObject hoverTurnEnd;

    [SerializeField]
    private Text turnEndText;

    private bool isActive = true;
   

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        title = "턴 종료(E)";
        contents = "이 버튼을 누르면 턴을\n종료합니다.\n\n손패를 버리고 적 턴을\n" +
            "진행한 뒤, 5 장의 카드를\n뽑고, 내 턴을 다시\n진행합니다.";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 액티브시 호버하면 불들어오고 팁 생김
        if(isActive)
        {
            hoverTurnEnd.SetActive(true);
            GameManager.UI.ShowTipUI(title, contents, ETipPos.Up, transform);
        }
        else
        {
            hoverTurnEnd.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverTurnEnd.SetActive(false);
        GameManager.UI.HideTipUI();
    }

    public void ActiveButton()
    {
        isActive = true;

        turnEndText.text = "턴 종료";
        image.color = originSpriteColor;
    }

    public void OnClickButtonEvent()
    {
        isActive = false;

        hoverTurnEnd.SetActive(false);
        GameManager.UI.HideTipUI();

        turnEndText.text = "적 턴";

        image.color = lockSpriteColor;
    }
}
