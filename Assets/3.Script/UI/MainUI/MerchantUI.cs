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

    // 상점 방에 들어올 때마다 값들 갱신
    public void Init()
    {
        // 지우는 값은 25원씩 비싸짐
        inDiscardUI.initDiscardCost += 25;
        inDiscardUI.isDiscard = false;

        inDiscardUI.Init();
    }

    public void ShowDiscardUI()
    {
        // 이미 팔린 상품이면 핀잔 줘야함
        if(inDiscardUI.isDiscard)
        {
            return;
        }


        // 돈이 있으면
        if(battleManager.Player.PlayerStat.Money >= inDiscardUI.initDiscardCost)
        {
            GameManager.UI.ShowUI(inDiscardUI);
        }
        else
        {
            // 돈이 없다고 핀잔 줘야함
        }
    }
}
