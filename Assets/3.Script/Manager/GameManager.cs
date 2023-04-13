using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private SoundManager _sound = new SoundManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private GameManagerEx _game = new GameManagerEx();
    private UIManager _ui = new UIManager();
    public static SoundManager Sound => Instance._sound;
    public static SceneManagerEx Scene => Instance._scene;
    public static GameManagerEx Game => Instance._game;
    public static UIManager UI => Instance._ui;

    private bool _isInit = false;

    public void Init()
    {

        if(!_isInit)
        {
            _isInit = true;

            _sound.Init();
            _scene.Init();
            _game.Init();
            _ui.Init();
        }
    }
}
