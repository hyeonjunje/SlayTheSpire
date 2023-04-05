using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private SoundManager _sound;
    public SoundManager Sound => _sound;
}
