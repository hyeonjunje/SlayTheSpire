using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMerchantUI : BaseUI
{
    [SerializeField] private Merchant merchant;

    public override void Hide()
    {
        base.Hide();

        merchant.gameObject.SetActive(false);
    }

    public override void Show()
    {
        base.Show();

        merchant.gameObject.SetActive(true);
    }
}
