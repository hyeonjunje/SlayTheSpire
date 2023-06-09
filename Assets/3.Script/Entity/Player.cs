using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public System.Action onDead;

    public PlayerStat PlayerStat { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public CharacterIndent CharacterIndent { get; private set; }
    public PlayerRelic PlayerRelic { get; private set; }
    
    public List<BaseCard> myCards;
    public CardHolder cardHolder;

    public void Init()
    {
        CharacterAnimation = GetComponent<CharacterAnimation>();
        PlayerStat = GetComponent<PlayerStat>();
        CharacterIndent = GetComponent<CharacterIndent>();
        PlayerRelic = GetComponent<PlayerRelic>();

        CharacterAnimation.Init(this);
        PlayerStat.Init(this);
        CharacterIndent.Init(this);
        PlayerRelic.Init(this);

        battleManager.onStartMyTurn += (() => PlayerStat.Shield = 0);
        battleManager.onStartMyTurn += (() => PlayerStat.CurrentOrb = PlayerStat.MaxOrb);

        battleManager.onEndMyTurn += (() => CharacterIndent.UpdateIndents());

        battleManager.onStartBattle += (() => OnStartBattle());
        battleManager.onEndBattle += (() => OnEndBattle());
    }

    public void OnStartBattle()
    {
        indent = new bool[(int)EIndent.Size];

        cardHolder.StartBattle(myCards);
        CharacterIndent.Visualize();
    }

    public void OnEndBattle()
    {
        indent = new bool[(int)EIndent.Size];
        CharacterIndent.ClearIndentList();
        cardHolder.EndBattle(myCards);
    }

    public void ResumeBattle()
    {
        cardHolder.ResumeBattle(myCards);
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

    public override void Dead()
    {
        Debug.Log("주겄당");
        CharacterAnimation.SetTrigger("isDead");

        // 죽으면 점수표 뜨고 로비로 돌아가기 창
        onDead?.Invoke();
    }

    public override void Hit(int damage, Character attacker)
    {
        // 약화 상태면 공격력 25퍼 감소
        if (attacker.indent[(int)EIndent.Weakening] == true)
        {
            damage = Mathf.RoundToInt((float)damage * 0.75f);
        }

        Debug.Log("맞았당");
        PlayerStat.Hit(damage);

        if (!PlayerStat.IsDead)
            CharacterAnimation.SetTrigger("isHitted");
    }

    public override void Act()
    {
        Debug.Log("행동한당");
        StartCoroutine(CharacterAnimation.CoAct(true));
        CharacterIndent.Visualize();
    }
}
