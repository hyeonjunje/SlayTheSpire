using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    // 플레이어 체력 UI 해줄려고 만든 델리게이트
    protected delegate void OnChangeHp();
    protected OnChangeHp onChangeHp;

    private Character _character;

    [Header("Stat")]
    [SerializeField] private int _maxHp;
    private int _currentHp;
    private int _shield;
    private int _power;
    private int _agility;

    [Header("UI")]
    [SerializeField] private HpBar _hpBar;

    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            int changeValue = value - _maxHp;
            _maxHp = value;

            // 최대 hp증가한 만큼 현재체력도 올라감
            if (changeValue > 0)
                CurrentHp += changeValue;

            onChangeHp?.Invoke();
        }
    }

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            // 체력바
            _currentHp = value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            _hpBar.DisplayHpBar(_currentHp, _maxHp);

            onChangeHp?.Invoke();

            if (_currentHp == 0)
            {
                Dead();
            }
        }
    }

    public int Shield
    {
        get { return _shield; }
        set
        {
            _shield = value;
            _shield = Mathf.Clamp(_shield, 0, 999);

            _hpBar.DisplayShield(_shield);
        }
    }

    public int Power { get; set; }
    public int Agility { get; set; }

    public bool IsDead => CurrentHp == 0;

    public virtual void Init(Character character)
    {
        CurrentHp = _maxHp;
        Shield = 0;
        this._character = character;
    }

    private void Dead()
    {
        _character.Dead();
    }

    public void Hit(int damage)
    {
        if(Shield > 0)
        {
            if(Shield >= damage)
            {
                Shield -= damage;
                damage = 0;
            }
            else
            {
                damage -= Shield;
                Shield = 0;
            }
        }
        CurrentHp -= damage;
    }
}
