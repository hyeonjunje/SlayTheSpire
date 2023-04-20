using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InBattleUI : BaseUI
{
    [SerializeField]
    private BattleTurnUI battleTurnUI; // 턴이 시작되면 나오는 UI
    [SerializeField]
    private TurnEndUI turnEndUI; // 턴을 끝낼 수 있는 버튼 UI

    // battleTurnUI가 비활성화 되면 플레이어와 적의 시작 턴이 종료됨
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

        // 카드 전투상태
        battleManager.Player.ResumeBattle();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
