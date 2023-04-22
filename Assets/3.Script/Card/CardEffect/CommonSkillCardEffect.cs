using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSkillCardEffect : BaseCardEffect
{
    // ����
    public void Defend()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    public void DefendPlus()
    {
        player.PlayerStat.Shield += (8 + agility);
    }

    // �������
    public void Armaments()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    public void ArmamentsPlus()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    // �� Ǯ��
    public void Flex()
    {
        
    }

    public void FlexPlus()
    {

    }

    // �ı�
    public void Havoc()
    {

    }

    public void HavocPlus()
    {

    }

    // ���������
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

    // ������ ����
    public void TrueGrit()
    {
        player.PlayerStat.Shield += (7 + agility);
    }

    public void TrueGritPlus()
    {
        player.PlayerStat.Shield += (9 + agility);
    }

    // ������ �Լ�
    public void Warcry()
    {
        player.cardHolder.DrawCard();
    }

    public void WarcryPlus()
    {
        player.cardHolder.DrawCard();
        player.cardHolder.DrawCard();
    }
}
