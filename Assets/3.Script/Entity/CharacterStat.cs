using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    // �÷��̾� ü�� UI ���ٷ��� ���� ��������Ʈ
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

            // �ִ� hp������ ��ŭ ����ü�µ� �ö�
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
            // ü�¹�
            _currentHp = value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            _hpBar.DisplayHpBar(_currentHp, _maxHp);

            onChangeHp?.Invoke();

            if (_currentHp <= 0)
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
            int shieldAmount = value - _shield;

            // ��ȭ�� �� ���� 25% ����
            if(shieldAmount > 0 && _character.indent[(int)EIndent.damaged])
            {
                shieldAmount = Mathf.RoundToInt((float)shieldAmount * 0.75f);
            }

            _shield += shieldAmount;
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
        // ����� �� ������ 1.5��� �� ����
        if(_character.indent[(int)EIndent.Weak])
        {
            damage = Mathf.RoundToInt((float)damage * 1.5f);
        }

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
