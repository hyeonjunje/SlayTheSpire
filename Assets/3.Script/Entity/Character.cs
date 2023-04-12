using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // 몬스터, 플레이어의 부모 스크립트

    /*
     hp가 있고 방어막
     */

    protected int _maxHp;
    protected int _currentHp;

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            // 체력바
            _currentHp = value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            if(_currentHp == 0)
            {
                Dead();
            }
        }
    }

    // 죽기
    public abstract void Dead();

    // 맞기
    public abstract void Hit();
}
