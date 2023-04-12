using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Neow : MonoBehaviour
{
    [SerializeField]
    private List<Dialog> dialogs = new List<Dialog>();

    [SerializeField]
    private Text speechBubbleText;
    [SerializeField]
    private Transform parent;

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
        GameManager.UI.ShowSelectedButton(dialogs[dialogIndex], parent);
        dialogIndex++;
    }

    public void EndConversation()
    {
        // 첫번째 스테이지 isgoable 처리
        GameManager.Game.StartMap();
        GameObject.Find("@Act1Scene").GetComponent<Act1Scene>().ShowMap();
    }
}
