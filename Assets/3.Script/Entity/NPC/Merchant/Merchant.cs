using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Merchant : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color originColor;
    [SerializeField] private SpriteRenderer merchantObject;

    [SerializeField] private MerchantUI merchantUI;

    // Ŭ���� ��
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.UI.ShowUI(merchantUI);
    }

    // ���콺 ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        merchantObject.color = hoverColor;
    }

    // ���콺 ȣ�� ����
    public void OnPointerExit(PointerEventData eventData)
    {
        merchantObject.color = originColor;
    }

    public void Init()
    {
        merchantUI.Init();

        // ������������ ȯ���ϴ� �Ҹ�
        GameManager.Sound.PlaySE(ESE.MerchantLaugh);
    }
}
