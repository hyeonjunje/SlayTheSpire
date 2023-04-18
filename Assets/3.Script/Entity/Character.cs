using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    // 몬스터, 플레이어의 부모 스크립트

    // 자신의 턴이 시작될 때 실행될 델리게이트
    public delegate void OnStartTurn();
    public OnStartTurn onStartTurn;

    // 자신의 턴이 끝날 때 실행될 델리게이트
    public delegate void OnEndTurn();
    public OnEndTurn onEndTurn;

    // 플레이어 체력 UI 해줄려고 만든 델리게이트
    protected delegate void OnChangeHp();
    protected OnChangeHp onChangeHp;

    [SerializeField]
    private HpBar _hpBar;

    protected Animator _animator;

    /*private int _animIsHittedHash = Animator.StringToHash("isHitted");
    private int _animIsDeadHash = Animator.StringToHash("isDead");*/

    /*
     hp가 있고 방어막
     */

    [SerializeField]
    protected int _maxHp;
    protected int _currentHp;

    protected int _shieldAmount;

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

    public bool IsDead => CurrentHp == 0;
    protected BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // 죽기
    public virtual void Dead()
    {
        // 죽는 애니메이션
        _animator.SetTrigger("isDead");
    }

    // 맞기
    public virtual void Hit(int damage)
    {
        if (IsDead)
            return;

        // 데미지 입기
        if (ShieldAmount > damage)
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

        if (IsDead)
            return;

        // 맞는 애니메이션
        _animator.SetTrigger("isHitted");
    }

    public abstract void Act();

    protected virtual void Awake()
    {
        CurrentHp = _maxHp;
        ShieldAmount = 0;

        _animator = GetComponent<Animator>();
    }

    protected IEnumerator CoAct(bool isRight)
    {
        Vector3 dir = isRight ? Vector3.right : Vector3.left;

        Vector3 origion = transform.position;

        float moveTime = 0.1f;
        float currentTime = 0f;

        while(true)
        {
            currentTime += Time.deltaTime;

            transform.position = Vector3.Lerp(origion, origion + dir, currentTime / moveTime);

            if(currentTime >= moveTime)
            {
                break;
            }

            yield return null;
        }

        currentTime = 0f;
        origion = transform.position;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.position = Vector3.Lerp(origion, origion - dir, currentTime / moveTime);

            if (currentTime >= moveTime)
            {
                break;
            }

            yield return null;
        }
    }
}
