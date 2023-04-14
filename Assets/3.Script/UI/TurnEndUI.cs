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
    
    
    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(() => OnClickButton());

        title = "�� ����(E)";
        contents = "�� ��ư�� ������ ����\n�����մϴ�.\n\n���и� ������ �� ����\n" +
            "������ ��, 5 ���� ī�带\n�̰�, �� ���� �ٽ�\n�����մϴ�.";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��Ƽ��� ȣ���ϸ� �ҵ����� �� ����
        if(BattleManager.Instance.MyTurn)
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
        turnEndText.text = "�� ����";
        image.color = originSpriteColor;
    }

    public void OnClickButtonEvent()
    {
        hoverTurnEnd.SetActive(false);
        GameManager.UI.HideTipUI();

        turnEndText.text = "�� ��";

        image.color = lockSpriteColor;
    }

    private void OnClickButton()
    {
        if(BattleManager.Instance.MyTurn)
        {
            BattleManager.Instance.MyTurn = false;
        }
    }
}
