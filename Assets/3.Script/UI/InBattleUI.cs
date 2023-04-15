using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InBattleUI : MonoBehaviour
{
    [SerializeField]
    private BattleManager battleManager;

    [SerializeField]
    private BattleTurnUI battleTurnUI; // ���� ���۵Ǹ� ������ UI
    [SerializeField]
    private TurnEndUI turnEndUI; // ���� ���� �� �ִ� ��ư UI

    // battleTurnUI�� ��Ȱ��ȭ �Ǹ� �÷��̾�� ���� ���� ���� �����
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

    // UI�� Ȱ��ȭ
    private void StartBattle()
    {

    }

    private void EndBattle()
    {

    }
}
