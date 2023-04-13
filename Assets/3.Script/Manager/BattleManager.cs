using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private Enemy targetEnemy = null;

    public Enemy TargetEnemy
    {
        get { return targetEnemy; }
        set
        {
            if (GameManager.Game.CardHolder.selectedCard == null)
                return;

            targetEnemy?.LockOff();
            targetEnemy = value;
            targetEnemy?.LockOn();

            // Ÿ���� ���� �ƴϸ� ������ � ���̶���Ʈ
            GameManager.Game.CardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }
}
