using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public PlayerStat PlayerStat { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }


    public List<BaseCard> myCards;
    public CardHolder cardHolder;

    private void Awake()
    {
        PlayerStat = GetComponent<PlayerStat>();
        CharacterAnimation = GetComponent<CharacterAnimation>();

        PlayerStat.Init(this);
        CharacterAnimation.Init(this);

        onStartTurn += (() => PlayerStat.Shield = 0);
        onStartTurn += (() => PlayerStat.CurrentOrb = PlayerStat.MaxOrb);
    }


    public void ResumeBattle()
    {
        cardHolder.ResumeBattle(myCards);
    }

    public void StartBattle()
    {
        cardHolder.StartBattle(myCards);
    }

    public void EndBattle()
    {
        cardHolder.EndBattle(myCards);
    }

    // �÷��̾��� ī�带 �����ش�.
    public void AddCard(BaseCard card)
    {
        myCards.Add(card);
    }

    // �÷��̾��� ī�带 �����Ѵ�.
    public void RemoveCard(BaseCard card)
    {

    }

    public override void Dead()
    {
        Debug.Log("�ְδ�");
        CharacterAnimation.SetTrigger("isDead");
    }

    public override void Hit(int damage)
    {

        Debug.Log("�¾Ҵ�");
        PlayerStat.Hit(damage);

        if (!PlayerStat.IsDead)
            CharacterAnimation.SetTrigger("isHitted");
    }

    public override void Act()
    {
        Debug.Log("�ൿ�Ѵ�");
        StartCoroutine(CharacterAnimation.CoAct(true));
    }
}
