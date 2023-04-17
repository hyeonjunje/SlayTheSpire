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

    public List<BaseCard> myCards;

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
        Debug.Log("으앙 주금");
    }

    public override void Hit(int damage)
    {
        Debug.Log("으앙 아파");
        base.Hit(damage);
    }

    public void InitShield()
    {
        // if 바리케이트 있으면 이건 안해
        ShieldAmount = 0;
    }

    public override void Act()
    {
        StartCoroutine(CoAct(true));
    }

    public void StartBattle()
    {
        _cardHolder.StartBattle(myCards);
    }

    public void EndBattle()
    {
        foreach(BaseCard card in myCards)
        {
            card.EndBattle();
        }
    }

    // 플레이어의 카드를 더해준다.
    public void AddCard(BaseCard card)
    {
        myCards.Add(card);
    }

    // 플레이어의 카드를 제거한다.
    public void RemoveCard(BaseCard card)
    {

    }
}
