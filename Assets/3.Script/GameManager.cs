using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private SoundManager _sound = new SoundManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    public SoundManager Sound => _sound;
    public SceneManagerEx Scene => _scene;

    public void Init()
    {
        _sound.Init();
        _scene.Init();
    }
}
