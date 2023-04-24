using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    [SerializeField]
    private Slider bgmVolume, seVolume;
    [SerializeField]
    private Text bgmVolumeText, seVolumeText;
    [SerializeField]
    private Toggle cameraShake, cheatMode;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show()
    {
        base.Show();
    }

    private void Awake()
    {
        bgmVolume.onValueChanged.AddListener(delegate { ValueChangeBGMVolume(); });
        seVolume.onValueChanged.AddListener(delegate { ValueChangeSEVolume(); });
        cameraShake.isOn = true;
        cameraShake.onValueChanged.AddListener(delegate { ValueChangeCameraShakeToggle(); });
        cheatMode.isOn = false;
        cheatMode.onValueChanged.AddListener(delegate { ValueChangeCheatToggle(); });
    }

    private void ValueChangeBGMVolume()
    {
        // 배경음 조절
        // text 표시
        GameManager.Sound.ChangeBGMVolume(bgmVolume.value);
        bgmVolumeText.text = Mathf.RoundToInt(bgmVolume.value * 100f) + "%";
    }

    private void ValueChangeSEVolume()
    {
        // 효과음 조절
        // text 표시
        GameManager.Sound.ChangeSEVolume(seVolume.value);
        seVolumeText.text = Mathf.RoundToInt(seVolume.value * 100f) + "%";
    }

    private void ValueChangeCameraShakeToggle()
    {
        WindowShake.Instance.isShake = cameraShake.isOn;
    }

    private void ValueChangeCheatToggle()
    {
        if(cheatMode.isOn)
        {
            battleManager.Player.PlayerStat.Power = 50;
            battleManager.Player.PlayerStat.Agility = 50;
        }
        else
        {
            battleManager.Player.PlayerStat.Power = 0;
            battleManager.Player.PlayerStat.Agility = 0;
        }
    }
}
