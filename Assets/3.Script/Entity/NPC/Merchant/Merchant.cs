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

    // 클릭할 시
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.UI.ShowUI(merchantUI);
    }

    // 마우스 호버
    public void OnPointerEnter(PointerEventData eventData)
    {
        merchantObject.color = hoverColor;
    }

    // 마우스 호버 종료
    public void OnPointerExit(PointerEventData eventData)
    {
        merchantObject.color = originColor;
    }

    public void Init()
    {
        merchantUI.Init();

        // 상점아저씨의 환영하는 소리
        GameManager.Sound.PlaySE(ESE.MerchantLaugh);
    }
}
