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
    CardSelect,      // 카드 onDragEnter 할 시
    BuyItem,         // 아이템 구매
    OpenTreasureBox, // 상자 열기
    PressEndButton,  // 내 턴 종료 버튼 누르기
    StartEnemyTurn,  // 적 턴 시작
    CardExhaust,     // 카드 소멸
    Heal,            // 회복
    ShowMap,         // 맵 UI킬 때
    EnterRoom,       // 방 들어갈 때
    StartMyTun,      // 내 턴 시작
    UpgradeCard,     // 업그레이드 카드
    CardHover,       // 카드 호버
    Sleep,           // 휴식처에서 자는 소리
    MerchantAngry,   // 상점 아저씨 화난 소리
    MerchantLaugh,   // 상점 아저씨 웃는 소리
    NormalAttack,    // 기본 공격
    StrongerAttack,  // 더 강한 공격
    HeavyAttack,     // 더 더 강한 공격
    GainDefense,     // 방어도 얻기
    DefenseBreak,    // 방어도 있는 적 때리기
    Buff,            // 버프
    Debuff,          // 디버프
    EnemyAttack,     // 적 공격
    SlimeAttack,     // 점액투성이 카드 넣기
    KingSlime,       // 킹슬라임 울음소리
    Size,
}

public class SoundManager
{
    private Dictionary<EBGM, AudioClip> _bgmDic;
    private Dictionary<ESE, AudioClip> _seDic;

    private AudioSource _bgmAudio = null;
    private AudioSource _seAudio = null;

    public float bgmVolume = 0.5f;
    public float seVolume = 0.5f;

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
            _seAudio = go1.AddComponent<AudioSource>();
            _seAudio.loop = false;
        }

        _bgmAudio.volume = bgmVolume;
        _seAudio.volume = seVolume;

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

        // 타이틀 씬
        _seDic[ESE.Heart] = Resources.Load<AudioClip>("SE/SLS_SFX_HeartBeat_Resonant_v1");
        _seDic[ESE.UIHover] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIHover_v2");
        _seDic[ESE.UIClick] = Resources.Load<AudioClip>("SE/SOTE_SFX_UIClick_2_v2");
        _seDic[ESE.IroncladSelected] = Resources.Load<AudioClip>("SE/SOTE_SFX_IronClad_Atk_RR1_v2");
        _seDic[ESE.SilentSelected] = Resources.Load<AudioClip>("SE/SOTE_SFX_FastAtk_v2");
        _seDic[ESE.DefectSelected] = Resources.Load<AudioClip>("SE/STS_SFX_DefectBeam_v1");
        _seDic[ESE.WatcherSelected] = Resources.Load<AudioClip>("SE/STS_SFX_Watcher-Select_v2");
        
        // act1 씬
        _seDic[ESE.CardSelect] = Resources.Load<AudioClip>("SE/SOTE_SFX_CardSelect_v2");
        _seDic[ESE.BuyItem] = Resources.Load<AudioClip>("SE/SOTE_SFX_CashRegister");
        _seDic[ESE.OpenTreasureBox] = Resources.Load<AudioClip>("SE/SOTE_SFX_ChestOpen_v2");
        _seDic[ESE.PressEndButton] = Resources.Load<AudioClip>("SE/SOTE_SFX_EndTurn_v2");
        _seDic[ESE.StartEnemyTurn] = Resources.Load<AudioClip>("SE/SOTE_SFX_EnemyTurn_v3");
        _seDic[ESE.CardExhaust] = Resources.Load<AudioClip>("SE/SOTE_SFX_ExhaustCard");
        _seDic[ESE.Heal] = Resources.Load<AudioClip>("SE/SOTE_SFX_HealShort_1_v2");
        _seDic[ESE.ShowMap] = Resources.Load<AudioClip>("SE/SOTE_SFX_Map_1_v3");
        _seDic[ESE.EnterRoom] = Resources.Load<AudioClip>("SE/SOTE_SFX_MapSelect_1_v1");
        _seDic[ESE.StartMyTun] = Resources.Load<AudioClip>("SE/SOTE_SFX_PlayerTurn_v4_1");
        _seDic[ESE.UpgradeCard] = Resources.Load<AudioClip>("SE/SOTE_SFX_UpgradeCard_v1");
        _seDic[ESE.CardHover] = Resources.Load<AudioClip>("SE/STS_SFX_CardHover3_v1");
        _seDic[ESE.Sleep] = Resources.Load<AudioClip>("SE/STS_SleepJingle_1a_NewMix_v1");
        _seDic[ESE.MerchantAngry] = Resources.Load<AudioClip>("SE/STS_VO_Merchant_2b");
        _seDic[ESE.MerchantLaugh] = Resources.Load<AudioClip>("SE/STS_VO_Merchant_3a");

        // 전투
        _seDic[ESE.NormalAttack] = Resources.Load<AudioClip>("SE/STS_SFX_DaggerThrow_2_2");
        _seDic[ESE.StrongerAttack] = Resources.Load<AudioClip>("SE/SOTE_SFX_IronClad_Atk_RR2_v2");
        _seDic[ESE.HeavyAttack] = Resources.Load<AudioClip>("SE/SOTE_SFX_HeavyBlunt_v2");
        _seDic[ESE.GainDefense] = Resources.Load<AudioClip>("SE/SOTE_SFX_GainDefense_RR1_v3");
        _seDic[ESE.DefenseBreak] = Resources.Load<AudioClip>("SE/SOTE_SFX_DefenseBreak_v2");
        _seDic[ESE.Buff] = Resources.Load<AudioClip>("SE/SOTE_SFX_Buff_1_v1");
        _seDic[ESE.Debuff] = Resources.Load<AudioClip>("SE/SOTE_SFX_Debuff_1_v1");

        // 적
        _seDic[ESE.EnemyAttack] = Resources.Load<AudioClip>("SE/STS_SFX_Shiv2_v1");
        _seDic[ESE.SlimeAttack] = Resources.Load<AudioClip>("SE/STS_SFX_SlimedAtk_v2");
        _seDic[ESE.KingSlime] = Resources.Load<AudioClip>("SE/STS_VO_SlimeBoss_1a");
    }

    // 효과음 재생
    public void PlaySE(ESE se)
    {
        _seAudio.PlayOneShot(_seDic[se]);
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

    public void ChangeBGMVolume(float value)
    {
        _bgmAudio.volume = value;
    }

    public void ChangeSEVolume(float value)
    {
        _seAudio.volume = value;
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
            _bgmAudio.volume = Mathf.Lerp(bgmVolume, 0, currentTime / (duration / 2));
            yield return null;
        }

        _bgmAudio.Stop();
        _bgmAudio.clip = clip;
        _bgmAudio.Play();

        // FadeIn
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _bgmAudio.volume = Mathf.Lerp(0, bgmVolume, currentTime / duration);
            yield return null;
        }
    }
}
