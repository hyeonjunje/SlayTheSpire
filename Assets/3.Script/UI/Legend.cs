using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
public enum ERoomType
{
    Elite = 0,
    Enemy = 1,
    Merchant = 2,
    Rest = 3,
    Treasure = 4,
    Unknown = 5,
    Size = 6
}
 */

public class Legend : MonoBehaviour
{
    [SerializeField]
    private Button[] legendButtons;

    [SerializeField]
    private StageData[] stageData;

    private void Awake()
    {
        for (int i = 0; i < legendButtons.Length; i++)
        {
            int index = i;
            legendButtons[i].onClick.AddListener(() => GameManager.Game.ShowRoomWithType((ERoomType)index));
            legendButtons[i].onClick.AddListener(() => GameManager.Game.ShowTipUI(stageData[index].roomName, stageData[index].roomExplanation, 
                ETipPos.Down, transform));
        }
    }
}
