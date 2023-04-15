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

        title = "�� ����(E)";
        contents = "�� ��ư�� ������ ����\n�����մϴ�.\n\n���и� ������ �� ����\n" +
            "������ ��, 5 ���� ī�带\n�̰�, �� ���� �ٽ�\n�����մϴ�.";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��Ƽ��� ȣ���ϸ� �ҵ����� �� ����
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

        turnEndText.text = "�� ����";
        image.color = originSpriteColor;
    }

    public void OnClickButtonEvent()
    {
        isActive = false;

        hoverTurnEnd.SetActive(false);
        GameManager.UI.HideTipUI();

        turnEndText.text = "�� ��";

        image.color = lockSpriteColor;
    }
}
