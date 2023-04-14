using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBarSlider;
    [SerializeField] private Text hpBarText;
    [SerializeField] private Image hpBarFill;
    [SerializeField] private GameObject hpBarShield;
    [SerializeField] private Text hpBarShieldText;
    [SerializeField] private GameObject outLine;

    [SerializeField] private Color originColor;
    [SerializeField] private Color shieldColor;

    private RectTransform hpBarSliderRectTransform;

    private void Awake()
    {
        hpBarSliderRectTransform = hpBarSlider.GetComponent<RectTransform>();
    }

    public void DisplayHpBar(int currentHp, int maxHp)
    {
        hpBarSlider.value = (float)currentHp / maxHp;
        hpBarText.text = currentHp + "/" + maxHp;
    }

    public void DisplayShield(int shieldAmount)
    {
        // 쉴드가 있으면
        if (shieldAmount > 0)
        {
            outLine.SetActive(true);

            hpBarFill.color = shieldColor;
            hpBarShield.gameObject.SetActive(true);
            hpBarShieldText.text = shieldAmount.ToString();
        }
        else
        {
            outLine.SetActive(false);

            hpBarFill.color = originColor;
            hpBarShield.gameObject.SetActive(false);
        }
    }
}
