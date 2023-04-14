using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // 몬스터, 플레이어의 부모 스크립트

    [SerializeField]
    private HpBar _hpBar;

    /*
     hp가 있고 방어막
     */

    [SerializeField]
    protected int _maxHp;
    protected int _currentHp;

    protected int _shieldAmount;

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            // 체력바
            _currentHp = value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            _hpBar.DisplayHpBar(_currentHp, _maxHp);

            if (_currentHp == 0)
            {
                Dead();
            }
        }
    }

    public int ShieldAmount
    {
        get { return _shieldAmount; }
        set
        {
            _shieldAmount = value;
            _shieldAmount = Mathf.Clamp(_shieldAmount, 0, 999);

            _hpBar.DisplayShield(_shieldAmount);
        }
    }

    // 죽기
    public abstract void Dead();

    // 맞기
    public virtual void Hit(int damage)
    {
        // 맞는 애니메이션


        // 데미지 입기
        if(ShieldAmount > damage)
        {
            ShieldAmount -= damage;
            damage = 0;
        }
        else
        {
            damage -= ShieldAmount;
            ShieldAmount = 0;
        }

        CurrentHp -= damage;
    }


    private void Awake()
    {
        CurrentHp = _maxHp;
        ShieldAmount = 0;
    }
}
