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

            // 타겟이 널이 아니면 베지어 곡선 하이라이트
            GameManager.Game.CardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }
}
