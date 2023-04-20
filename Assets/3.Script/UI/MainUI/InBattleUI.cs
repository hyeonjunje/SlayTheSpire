using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InBattleUI : BaseUI
{
    [SerializeField]
    private BattleTurnUI battleTurnUI; // ���� ���۵Ǹ� ������ UI
    [SerializeField]
    private TurnEndUI turnEndUI; // ���� ���� �� �ִ� ��ư UI

    // battleTurnUI�� ��Ȱ��ȭ �Ǹ� �÷��̾�� ���� ���� ���� �����
    public bool EndStartTurn => !battleTurnUI.gameObject.activeSelf;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    private void Awake()
    {
        battleManager.onStartMyTurn += (() => battleTurnUI.gameObject.SetActive(true));
        battleManager.onStartEnemyTurn += (() => battleTurnUI.gameObject.SetActive(true));
        battleManager.onStartMyTurn += battleTurnUI.DisplayBattleMyTurn;
        battleManager.onStartEnemyTurn += battleTurnUI.DisplayBattleEnemyTurn;

        battleManager.onStartMyTurn += turnEndUI.ActiveButton;
        battleManager.onStartEnemyTurn += turnEndUI.OnClickButtonEvent;
    }


    public override void Show()
    {
        base.Show();

        // ī�� ��������
        battleManager.Player.ResumeBattle();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
