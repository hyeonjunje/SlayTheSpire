using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBGM
{
    Menu,
    Level1
        
}

public class SoundManager
{
    private Dictionary<EBGM, AudioClip> _dictionary;

    private AudioSource _bgmAudio = null;

    public void Init()
    {
        if(_bgmAudio == null)
        {
            GameObject go = new GameObject(typeof(SoundManager).Name);
            go.transform.parent = GameManager.Instance.transform;
            _bgmAudio = go.AddComponent<AudioSource>();
        }

        _dictionary = new Dictionary<EBGM, AudioClip>();

        _dictionary[EBGM.Menu] = Resources.Load<AudioClip>("music/STS_MenuTheme_NewMix_v1");
        _dictionary[EBGM.Level1] = Resources.Load<AudioClip>("music/STS_Level1_NewMix_v1");
    }

    public void PlayBGM(EBGM bgm)
    {
        if(!_dictionary.ContainsKey(bgm))
        {
            Debug.Log("없는 bgm 입니다.");
            return;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = _dictionary[bgm];
        _bgmAudio.Play();
    }
}
