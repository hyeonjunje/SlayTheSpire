using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardEffect : BaseCardEffect
{
    [SerializeField]
    private IndentData[] indentData;

    // 수비
    public void Defend()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    public void DefendPlus()
    {
        player.PlayerStat.Shield += (8 + agility);
    }


    // 흘려보내기
    public void ShrugItOff()
    {
        player.PlayerStat.Shield += (8 + agility);
        player.cardHolder.DrawCard();
    }

    public void ShrugItOffPlus()
    {
        player.PlayerStat.Shield += (11 + agility);
        player.cardHolder.DrawCard();
    }

    // 사혈
    public void Bloodletting()
    {
        player.PlayerStat.CurrentOrb += 2;
        player.PlayerStat.CurrentHp -= 3;
    }

    public void BloodlettingPlus()
    {
        player.PlayerStat.CurrentOrb += 3;
        player.PlayerStat.CurrentHp -= 3;
    }

    // 위압
    public void Intimidate()
    {
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 1));
    }
    public void IntimidatePlus()
    {
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 2));
    }

    // 참호
    public void Entrench()
    {
        player.PlayerStat.Shield += player.PlayerStat.Shield;
    }

    public void EntrenchPlus()
    {
        player.PlayerStat.Shield += player.PlayerStat.Shield;
    }

    // 충격파
    public void ShockWave()
    {
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 3));
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 3));
    }
    public void ShockWavePlus()
    {
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weak], 5));
        enemies.ForEach(enemy => enemy.CharacterIndent.AddIndent(indentData[(int)EIndent.Weakening], 5));
    }

    // 혈안
    public void SeeingRed()
    {
        player.PlayerStat.CurrentOrb += 2;
    }

    public void SeeingRedPlus()
    {
        player.PlayerStat.CurrentOrb += 2;
    }

    // 무적
    public void Impervious()
    {
        player.PlayerStat.Shield += 30;
    }
    public void ImperviousPlus()
    {
        player.PlayerStat.Shield += 40;
    }

    // 제물
    public void Offering()
    {
        player.PlayerStat.CurrentHp -= 6;
        player.PlayerStat.CurrentOrb += 2;
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
    }

    public void OfferingPlus()
    {
        player.PlayerStat.CurrentHp -= 6;
        player.PlayerStat.CurrentOrb += 2;
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
    }
}
