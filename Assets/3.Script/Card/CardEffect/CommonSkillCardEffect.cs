using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSkillCardEffect : BaseCardEffect
{
    // 수비
    public void Defend()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    // 전투장비
    public void Armaments()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    // 몸 풀기
    public void Flex()
    {
        
    }

    // 파괴
    public void Havoc()
    {

    }

    // 흘려보내기
    public void ShrugItOff()
    {
        player.PlayerStat.Shield += (8 + agility);
        player.cardHolder.DrawCard();
    }

    // 진정한 끈기
    public void TrueGrit()
    {
        player.PlayerStat.Shield += (7 + agility);
    }

    // 전투의 함성
    public void Warcry()
    {
        player.cardHolder.DrawCard();
    }
}
