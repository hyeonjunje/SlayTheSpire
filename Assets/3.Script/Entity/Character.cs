using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // ����, �÷��̾��� �θ� ��ũ��Ʈ

    /*
     hp�� �ְ� ��
     */

    protected int _maxHp;
    protected int _currentHp;

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            // ü�¹�
            _currentHp = value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            if(_currentHp == 0)
            {
                Dead();
            }
        }
    }

    // �ױ�
    public abstract void Dead();

    // �±�
    public abstract void Hit();
}
