using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAddCard
{
    Deck,
    Hands,
    Cemetry
}

public class BaseCardEffect : MonoBehaviour
{
    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    protected Player player => battleManager.Player;
    protected List<Enemy> enemies => battleManager.Enemies;
    protected Enemy targetEnemy => battleManager.TargetEnemy;

    protected int power => player.PlayerStat.Power;
    protected int agility => player.PlayerStat.Agility;
}
