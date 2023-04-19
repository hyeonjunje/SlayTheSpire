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

    // �������
    public void Armaments()
    {
        player.PlayerStat.Shield += (5 + agility);
    }

    // �� Ǯ��
    public void Flex()
    {
        
    }

    // �ı�
    public void Havoc()
    {

    }

    // ���������
    public void ShrugItOff()
    {
        player.PlayerStat.Shield += (8 + agility);
        player.cardHolder.DrawCard();
    }

    // ������ ����
    public void TrueGrit()
    {
        player.PlayerStat.Shield += (7 + agility);
    }

    // ������ �Լ�
    public void Warcry()
    {
        player.cardHolder.DrawCard();
    }
}
