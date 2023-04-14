using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // ����, �÷��̾��� �θ� ��ũ��Ʈ

    [SerializeField]
    private HpBar _hpBar;

    /*
     hp�� �ְ� ��
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
            // ü�¹�
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

    // �ױ�
    public abstract void Dead();

    // �±�
    public virtual void Hit(int damage)
    {
        // �´� �ִϸ��̼�


        // ������ �Ա�
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
