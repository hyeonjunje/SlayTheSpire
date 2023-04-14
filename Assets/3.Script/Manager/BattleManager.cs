using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private Player _player;
    private List<Enemy> _enemies;

    public Player Player => _player;
    private List<Enemy> Enemies => _enemies;

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

    public void StartBattle(Player player, List<Enemy> enemies)
    {
        _player = player;
        _enemies = enemies;
    }
}
