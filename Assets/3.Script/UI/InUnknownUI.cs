using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InUnknownUI : MonoBehaviour
{
    [SerializeField]
    private Text eventName;
    [SerializeField]
    private Image eventImage;
    [SerializeField]
    private Text eventContents;

    [SerializeField]
    private Button[] optionsButton;

    public void ShowUnknown(UnknownData unknownData)
    {
        // 초기화
        Init();

        eventName.text = unknownData.roomName;
        eventImage.sprite = unknownData.roomImage;
        eventContents.text = unknownData.roomContents;

        for(int i = 0; i < unknownData.optionText.Length; i++)
        {
            optionsButton[i].gameObject.SetActive(true);

            int index = i;

            // 버튼 onclick, text 적용
            optionsButton[index].onClick.AddListener(() => unknownData.optionEvent[index].Invoke());
            optionsButton[i].GetComponentInChildren<Text>().text = unknownData.optionText[i];
        }
    }

    private void Init()
    {
        for(int i = 0; i < optionsButton.Length; i++)
        {
            optionsButton[i].onClick.RemoveAllListeners();
            optionsButton[i].gameObject.SetActive(false);
        }
    }
}
