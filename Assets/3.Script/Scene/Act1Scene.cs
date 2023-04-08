using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1Scene : BaseScene
{
    public override void Init()
    {
        GameManager.Sound.PlayBGM(EBGM.Level1);
    }
}
