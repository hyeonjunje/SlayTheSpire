using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRestUI : BaseUI
{
    [SerializeField] private InEnforceUI inEnforceUI;

    [SerializeField] private GameObject originBackground, bonfireBackgorund;

    [SerializeField] private Image ironcladImage;
    [SerializeField] private Sprite brightIroncladImage, darkIroncladImage;

    [SerializeField] private GameObject progressButton, restText;
    [SerializeField] private GameObject restButton, enforceButton, bonfire;

    private BattleManager _battleManager => ServiceLocator.Instance.GetService<BattleManager>();

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

        inEnforceUI.onEnforce = null;
        inEnforceUI.onEnforce += (() => IsUsed = true);
    }

    // 휴식
    public void Rest()
    {
        Debug.Log("쉽니다.");
        IsUsed = true;

        // 최대 체력의 30퍼센트를 회복합니다.
        _battleManager.Player.PlayerStat.CurrentHp += Mathf.RoundToInt(_battleManager.Player.PlayerStat.MaxHp * 0.3f);
    }

    // 강화
    public void Enforce()
    {
        Debug.Log("강화합니다.");

        GameManager.UI.ShowUI(inEnforceUI);
    }
}
