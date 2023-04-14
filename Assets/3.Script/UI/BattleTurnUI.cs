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

    public void DisplayBattleTurn(EBattleState battleState, int turnCount = 0)
    {
        if(battleState == EBattleState.MyTurnStart)
        {
            myTurn.SetActive(true);
            myTurnCountText.text = turnCount + "ео";
        }
        else if(battleState == EBattleState.EnemyTurnStart)
        {
            enemyTurn.SetActive(true);
        }
    }

    public void SetActiveFalse()
    {
        myTurn.SetActive(false);
        enemyTurn.SetActive(false);
        gameObject.SetActive(false);
    }
}
