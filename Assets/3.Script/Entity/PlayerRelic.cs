using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRelic : MonoBehaviour
{
    private Player _player;
    private bool[] _isRelics;

    [SerializeField]
    private IndentData weakIndentData;  // 취약
    [SerializeField]
    private IndentData powerIndentData;  // 힘
    [SerializeField]
    private IndentData agilityIndentData;  // 민첩

    private RelicGenerator relicGenerator => ServiceLocator.Instance.GetService<RelicGenerator>();

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void Init(Player player)
    {
        _player = player;
        _isRelics = new bool[(int)ERelic.Size];

        AddRelic(ERelic.BurningBlood);
    }

    public void AddRelic(ERelic relic)
    {
        // 없는 유물이라면
        if(_isRelics[(int)relic] == false)
        {
            // 유물 체크하고
            _isRelics[(int)relic] = true;

            // 유물 생성
            relicGenerator.GenerateRelic(relic);

            // 얻으면 효과가 발동하는 유물
            GainRelicAbility(relic);
        }
    }

    public bool GetRelic(ERelic relic)
    {
        return _isRelics[(int)relic];
    }

    // 이벤트로 처리할 수 있거나 바로 처리할 수 있는 이벤트는 얻자마자 설정해줌
    private void GainRelicAbility(ERelic relic)
    {
        switch (relic)
        {
            case ERelic.BurningBlood:
                battleManager.onEndBattle += (() => _player.PlayerStat.CurrentHp += 6);
                break;
            case ERelic.Vajra:
                battleManager.onFirstMyTurn += (() => _player.CharacterIndent.AddIndent(powerIndentData, 1));
                _player.PlayerStat.Power += 1;
                break;
            case ERelic.BagOfMarbles:
                battleManager.onFirstMyTurn += (() => battleManager.Enemies.ForEach(enemy =>
                enemy.CharacterIndent.AddIndent(weakIndentData, 1)));
                break;
            case ERelic.Anchor:
                battleManager.onFirstMyTurn += (() => _player.PlayerStat.Shield += 10);
                break;
            case ERelic.Strawberry:
                _player.PlayerStat.MaxHp += 7;
                break;
            case ERelic.SmoothStone:
                battleManager.onFirstMyTurn += (() => _player.CharacterIndent.AddIndent(agilityIndentData, 1));
                _player.PlayerStat.Agility += 1;
                break;
            case ERelic.BagOfPreparation:
                battleManager.onFirstMyTurn += (() => _player.cardHolder.DrawCard());
                battleManager.onFirstMyTurn += (() => _player.cardHolder.DrawCard());
                break;
            case ERelic.BloodVial:
                battleManager.onEndBattle += (() => _player.PlayerStat.CurrentHp += 2);
                break;
            case ERelic.RegalPilow:  // 따로 구현
                break;
            case ERelic.Lantern:
                battleManager.onFirstMyTurn += (() => _player.PlayerStat.CurrentOrb += 1);
                break;
            case ERelic.Meat:
                System.Action action = new System.Action(() =>
                {
                    if (_player.PlayerStat.CurrentHp <= _player.PlayerStat.MaxHp / 2)
                        _player.PlayerStat.CurrentHp += 12;
                });
                battleManager.onEndBattle += (() => action());
                break;
            case ERelic.Pear:
                _player.PlayerStat.MaxHp += 10;
                break;
            case ERelic.HornCleat:
                battleManager.onSecondMyTurn += (() => _player.PlayerStat.Shield += 14);
                break;
            case ERelic.Mango:
                _player.PlayerStat.MaxHp += 14;
                break;
            case ERelic.Ginger:  // 따로 구현
                break;
            case ERelic.Turnip:  // 따로 구현
                break;
            case ERelic.Torii:   // 따로 구현
                break;
        }
    }
}
