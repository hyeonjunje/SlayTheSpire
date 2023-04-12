using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Neow : MonoBehaviour
{
    [SerializeField]
    private List<Dialog> dialogs = new List<Dialog>();

    [SerializeField]
    private Text speechBubbleText;

    private int dialogIndex = 0;

    private void Awake()
    {
        dialogIndex = 0;

        List<string> answers = new List<string>();
        answers.Add("[대화한다]");
        dialogs.Add(new Dialog("한번 더 \n 도전할텐가...?", answers, ProceedConversation));

        answers = new List<string>();
        answers.Add("[다음 세 전투를 적들의 체력이 1인 상태로 시작합니다]");
        answers.Add("[최대 체력 +7]");
        dialogs.Add(new Dialog("선택하여라...", answers, ProceedConversation));

        answers = new List<string>();
        answers.Add("[떠난다]");
        dialogs.Add(new Dialog("허락하마...", answers, EndConversation));

        ProceedConversation();
    }

    public void ProceedConversation()
    {
        Debug.Log(dialogIndex + " 0 ");

        /*for(int i = 0; i < Act1Scene.Instance.buttons.Length; i++)
        {
            Act1Scene.Instance.buttons[i].gameObject.SetActive(false);
            Act1Scene.Instance.buttons[i].onClick.RemoveAllListeners();
        }

        speechBubbleText.text = dialogs[dialogIndex].contents;

        for(int i = 0; i < dialogs[dialogIndex].Count; i++)
        {
            Act1Scene.Instance.buttons[i].gameObject.SetActive(true);
            
            Act1Scene.Instance.buttons[i].GetComponentInChildren<Text>().text = dialogs[dialogIndex].answers[i];
            Act1Scene.Instance.buttons[i].onClick.AddListener(() => dialogs[dialogIndex - 1].onClickButtons());
        }*/

        dialogIndex++;
    }

    public void EndConversation()
    {
        // 첫번째 스테이지 isgoable 처리
        GameManager.Game.StartMap();
        GameObject.Find("@Act1Scene").GetComponent<Act1Scene>().ShowMap();
    }
}
