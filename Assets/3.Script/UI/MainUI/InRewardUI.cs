using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRewardUI : BaseUI
{
    [SerializeField]
    private Button passButton;
    [SerializeField]
    private GameObject cardReward;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    private void Awake()
    {
        passButton.onClick.AddListener(() => cardReward.SetActive(false));
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
