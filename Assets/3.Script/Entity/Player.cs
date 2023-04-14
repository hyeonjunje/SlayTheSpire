using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public delegate void OnDieEvent();
    public OnDieEvent onDieEvent;

    [SerializeField]
    private CardHolder _cardHolder;
    [SerializeField]
    private Text energyText;

    public CardHolder CardHolder => _cardHolder;

    public int maxOrb = 3;
    private int _orb;
    public int Orb
    {
        get { return _orb; }
        set
        {
            _orb = value;

            _orb = Mathf.Clamp(_orb, 0, 99);

            // UI
            energyText.text = _orb + "/" + maxOrb;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        onStartTurn += InitShield;
        onStartTurn += (() => Orb = maxOrb);
    }

    public override void Dead()
    {
        onDieEvent?.Invoke();
        Debug.Log("���� �ֱ�");
    }

    public override void Hit(int damage)
    {
        Debug.Log("���� ����");
        base.Hit(damage);
    }

    public void InitShield()
    {
        // if �ٸ�����Ʈ ������ �̰� ����
        ShieldAmount = 0;
    }

    public override void Act()
    {
        StartCoroutine(CoAct(true));
    }
}
