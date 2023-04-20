using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnUI : MonoBehaviour
{
    [SerializeField]
    private GameObject myTurn, enemyTurn;

    [SerializeField]
    private Text myTurnCountText;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void DisplayBattleMyTurn()
    {
        myTurn.SetActive(true);
        myTurnCountText.text = battleManager.myTurnCount + "턴";
    }

    public void DisplayBattleEnemyTurn()
    {
        enemyTurn.SetActive(true);
    }


    // 애니메이션 이벤트로 넣어둠
    private void SetActiveFalse()
    {
        myTurn.SetActive(false);
        enemyTurn.SetActive(false);
        gameObject.SetActive(false);
    }
}
