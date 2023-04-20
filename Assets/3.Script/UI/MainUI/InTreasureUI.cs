using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ETreasureType
{
    small,
    medium,
    large,
    Size
}

public class InTreasureUI : BaseUI
{
    [SerializeField] private Sprite smallChest, smallChestOpened;
    [SerializeField] private Sprite mediumChest, mediumChestOpened;
    [SerializeField] private Sprite largeChest, largeChestOpened;

    [SerializeField] private Image chestImage;

    private ETreasureType treasureType;

    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    private bool isUsed = false;
    public bool IsUsed
    {
        get { return isUsed; }
        set
        {
            isUsed = value;

            if (isUsed)
            {
                switch (treasureType)
                {
                    case ETreasureType.small:
                        chestImage.sprite = smallChestOpened;
                        break;
                    case ETreasureType.medium:
                        chestImage.sprite = mediumChestOpened;
                        break;
                    case ETreasureType.large:
                        chestImage.sprite = largeChestOpened;
                        break;
                }
            }
            else
            {
                treasureType = (ETreasureType)Random.Range(0, (int)ETreasureType.Size);

                switch (treasureType)
                {
                    case ETreasureType.small:
                        chestImage.sprite = smallChest;
                        break;
                    case ETreasureType.medium:
                        chestImage.sprite = mediumChest;
                        break;
                    case ETreasureType.large:
                        chestImage.sprite = largeChest;
                        break;
                }
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
    }

    public void OpenTreasure()
    {
        Debug.Log("보상을 줍니다.");
        IsUsed = true;

        // 보상창
        rewardManager.ShowReward();
    }
}
