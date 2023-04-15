using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InBattleUI : MonoBehaviour
{
    [SerializeField]
    private BattleManager battleManager;

    [SerializeField]
    private BattleTurnUI battleTurnUI; // 턴이 시작되면 나오는 UI
    [SerializeField]
    private TurnEndUI turnEndUI; // 턴을 끝낼 수 있는 버튼 UI

    // battleTurnUI가 비활성화 되면 플레이어와 적의 시작 턴이 종료됨
    public bool EndStartTurn => !battleTurnUI.gameObject.activeSelf;

    private void Awake()
    {
        battleManager.onStartMyTurn += (() => battleTurnUI.gameObject.SetActive(true));
        battleManager.onStartEnemyTurn += (() => battleTurnUI.gameObject.SetActive(true));
        battleManager.onStartMyTurn += battleTurnUI.DisplayBattleMyTurn;
        battleManager.onStartEnemyTurn += battleTurnUI.DisplayBattleEnemyTurn;

        battleManager.onStartMyTurn += turnEndUI.ActiveButton;
        battleManager.onStartEnemyTurn += turnEndUI.OnClickButtonEvent;
    }

    private void OnEnable()
    {
        StartBattle();
    }

    private void OnDisable()
    {
        EndBattle();
    }

    // UI들 활성화
    private void StartBattle()
    {

    }

    private void EndBattle()
    {

    }
}
