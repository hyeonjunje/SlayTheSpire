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
    private Toggle cameraShake;

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
    }

    private void ValueChangeBGMVolume()
    {
        // ����� ����
        // text ǥ��
        GameManager.Sound.ChangeBGMVolume(bgmVolume.value);
        bgmVolumeText.text = Mathf.RoundToInt(bgmVolume.value * 100f) + "%";
    }

    private void ValueChangeSEVolume()
    {
        // ȿ���� ����
        // text ǥ��
        GameManager.Sound.ChangeSEVolume(seVolume.value);
        seVolumeText.text = Mathf.RoundToInt(seVolume.value * 100f) + "%";
    }

    private void ValueChangeCameraShakeToggle()
    {
        WindowShake.Instance.isShake = cameraShake.isOn;
    }
}
