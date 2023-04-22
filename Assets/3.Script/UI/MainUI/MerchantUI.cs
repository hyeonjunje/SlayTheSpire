using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUI : BaseUI
{
    [SerializeField]
    private InDiscardUI inDiscardUI;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
    }

    // ���� �濡 ���� ������ ���� ����
    public void Init()
    {
        // ����� ���� 25���� �����
        inDiscardUI.initDiscardCost += 25;
        inDiscardUI.isDiscard = false;

        inDiscardUI.Init();
    }

    public void ShowDiscardUI()
    {
        // �̹� �ȸ� ��ǰ�̸� ���� �����
        if(inDiscardUI.isDiscard)
        {
            return;
        }


        // ���� ������
        if(battleManager.Player.PlayerStat.Money >= inDiscardUI.initDiscardCost)
        {
            GameManager.UI.ShowUI(inDiscardUI);
        }
        else
        {
            // ���� ���ٰ� ���� �����
        }
    }
}
