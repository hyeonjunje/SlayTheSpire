using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// BGM
public enum EBGM
{
    Menu,
    Level1,
    Merchant,
    Rest,
    EliteMeet,
    PlayerDead,
    BossMeet,
    BossClear,
    Size
}

// 효과음
public enum ESE
{
    Heart,
    UIHover,
    UIClick,
    IroncladSelected,
    SilentSelected,
    DefectSelected,
    WatcherSelected,
    Size,
}

public class SoundManager
{
    private Dictionary<EBGM, AudioClip> _bgmDic;
    private Dictionary<ESE, AudioClip> _seDic;

    private AudioSource _bgmAudio = null;
    private AudioSource _effectSound = null;

    public float bgmVolme = 0.5f;

    public void Init()
    {
        if(_bgmAudio == null)
        {
            GameObject go = new GameObject(typeof(SoundManager).Name);
            go.transform.parent = GameManager.Instance.transform;
            _bgmAudio = go.AddComponent<AudioSource>();
            _bgmAudio.loop = true;
            GameObject go1 = new GameObject("EffectSound");
            go1.transform.parent = GameManager.Instance.transform;
            _effectSound = go1.AddComponent<AudioSource>();
            _effectSound.loop = false;
        }

        _bgmAudio.volume = bgmVolme;

        _bgmDic = new Dictionary<EBGM, AudioClip>();

        _bgmDic[EBGM.Menu] = Resources.Load<AudioClip>("music/STS_MenuTheme_NewMix_v1");
        _bgmDic[EBGM.Level1] = Resources.Load<AudioClip>("music/STS_Level1_NewMix_v1");
        _bgmDic[EBGM.Merchant] = Resources.Load<AudioClip>("music/STS_Merchant_NewMix_v1");
        _bgmDic[EBGM.EliteMeet] = Resources.Load<AudioClip>("music/STS_EliteBoss_NewMix_v1");
        _bgmDic[EBGM.PlayerDead] = Resources.Load<AudioClip>("music/STS_DeathStinger_1_v3_MUSIC");
        _bgmDic[EBGM.BossMeet] = Resources.Load<AudioClip>("music/STS_Boss1_NewMix_v1");
        _bgmDic[EBGM.BossClear] = Resources.Load<AudioClip>("music/STS_BossVictoryStinger_1_v3_MUSIC");
        _bgmDic[EBGM.Rest] = Resources.Load<AudioClip>("music/SOTE_SFX_RestFireDry_v2");


        _seDic = new Dictionary<ESE, AudioClip>();

        _seDic[ESE.Heart] = Resources.Load<AudioClip>("SE/SLS_SFX_HeartBeat_Resonant_v1");
        _seDic[ESE.UIHover] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIHover_v2");
        _seDic[ESE.UIClick] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIClick_2_v2");
        _seDic[ESE.IroncladSelected] = Resources.Load<AudioClip>("SE/SOTE_SFX_IronClad_Atk_RR1_v2");
        _seDic[ESE.SilentSelected] = Resources.Load<AudioClip>("SE/SOTE_SFX_FastAtk_v2");
        _seDic[ESE.DefectSelected] = Resources.Load<AudioClip>("SE/STS_SFX_DefectBeam_v1");
        _seDic[ESE.WatcherSelected] = Resources.Load<AudioClip>("SE/STS_SFX_Watcher-Select_v2");
    }

    // 효과음 재생
    public void PlaySE(ESE se)
    {
        _effectSound.PlayOneShot(_seDic[se]);
    }

    // 배경음 재생
    public void PlayBGM(EBGM bgm)
    {
        if(!_bgmDic.ContainsKey(bgm))
        {
            Debug.Log("없는 bgm 입니다.");
            return;
        }

        if (_bgmAudio.clip == _bgmDic.ContainsKey(bgm))
        {
            Debug.Log("동일한 clip입니다.");
            return;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = _bgmDic[bgm];
        _bgmAudio.Play();
    }

    // 배경음 중단
    public void StopBgm()
    {
        _bgmAudio.Stop();
    }

    // 배경음 fade in, out 재생
    public IEnumerator FadeInOutAudioSource(EBGM bgm, float duration = 1.5f)
    {
        if (!_bgmDic.ContainsKey(bgm))
        {
            Debug.Log("없는 bgm 입니다.");
            yield break;
        }

        if (_bgmAudio.clip == null)
        {
            Debug.Log("현재 clip이 없습니다.");
            PlayBGM(bgm);
            yield break;
        }

        if(_bgmAudio.clip == _bgmDic[bgm])
        {
            Debug.Log("동일한 clip입니다.");
            yield break;
        }

        float currentTime = 0;
        AudioClip clip = _bgmDic[bgm];

        // FadeOut
        while(currentTime < duration / 2)
        {
            currentTime += Time.deltaTime;
            _bgmAudio.volume = Mathf.Lerp(bgmVolme, 0, currentTime / (duration / 2));
            yield return null;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = clip;
        _bgmAudio.Play();

        // FadeIn
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _bgmAudio.volume = Mathf.Lerp(0, bgmVolme, currentTime / duration);
            yield return null;
        }
    }
}
