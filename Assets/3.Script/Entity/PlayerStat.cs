using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : CharacterStat
{
    [SerializeField] private Text hpText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text energyText;
    [SerializeField] private Text stairText;

    private int _money;
    private int _maxOrb;
    private int _currentOrb;
    private int _height;

    public int MaxOrb
    {
        get { return _maxOrb; }
        set
        {
            _maxOrb = value;
            _maxOrb = Mathf.Clamp(_maxOrb, 0, 99);
            energyText.text = _currentOrb + "/" + _maxOrb;
        }
    }

    public int CurrentOrb
    {
        get { return _currentOrb; }
        set
        {
            _currentOrb = value;
            _currentOrb = Mathf.Clamp(_currentOrb, 0, 99);
            energyText.text = _currentOrb + "/" + _maxOrb;
        }
    }

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _money = Mathf.Clamp(_money, 0, 9999);
            moneyText.text = _money.ToString();
        }
    }

    public int Height
    {
        get { return _height; }
        set
        {
            _height = value;
            GameManager.Game.height = _height;

            stairText.text = _height.ToString();
        }
    }

    public override void Init(Character character)
    {
        base.Init(character);

        Height = 0;
        MaxOrb = 3;
        CurrentOrb = MaxOrb;
        Money = 99;
        onChangeHp += (() => hpText.text = CurrentHp + "/" + MaxHp);
    }
}
