using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : BaseUI
{
    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();

        GameManager.Sound.PlaySE(ESE.ShowMap);
    }
}
