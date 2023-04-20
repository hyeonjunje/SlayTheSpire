using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRestUI : BaseUI
{
    [SerializeField] private GameObject originBackground, bonfireBackgorund;

    [SerializeField] private Image ironcladImage;
    [SerializeField] private Sprite brightIroncladImage, darkIroncladImage;

    [SerializeField] private GameObject progressButton, restText;
    [SerializeField] private GameObject restButton, enforceButton, bonfire;

    private bool isUsed = false;
    public bool IsUsed
    {
        get { return isUsed; }
        set
        {
            isUsed = value;

            if(isUsed)
            {
                bonfire.SetActive(false);
                restText.SetActive(false);
                restButton.SetActive(false);
                enforceButton.SetActive(false);
                progressButton.gameObject.SetActive(true);

                ironcladImage.sprite = darkIroncladImage;

                GameManager.Game.CurrentRoom.ClearRoom();
            }
            else
            {
                bonfire.SetActive(true);
                restText.SetActive(true);
                restButton.SetActive(true);
                enforceButton.SetActive(true);
                progressButton.gameObject.SetActive(false);

                ironcladImage.sprite = brightIroncladImage;
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
        bonfireBackgorund.SetActive(false);
        originBackground.SetActive(true);
    }

    public override void Show()
    {
        base.Show();
        bonfireBackgorund.SetActive(true);
        originBackground.SetActive(false);
    }

    // �޽�
    public void Rest()
    {
        Debug.Log("���ϴ�.");
        IsUsed = true;
    }

    // ��ȭ
    public void Enforce()
    {
        Debug.Log("��ȭ�մϴ�.");
        IsUsed = true;
    }
}
