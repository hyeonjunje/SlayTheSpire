using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Neow : MonoBehaviour
{
    [SerializeField]
    private BaseUI mapUI;

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
        answers.Add("[��ȭ�Ѵ�]");
        dialogs.Add(new Dialog("�ݰ���...", answers, ProceedConversation));

        answers = new List<string>();
        answers.Add("[���� �� ������ ������ ü���� 1�� ���·� �����մϴ�]");
        answers.Add("[�ִ� ü�� +7]");
        dialogs.Add(new Dialog("�����Ͽ���...", answers, ProceedConversation));

        answers = new List<string>();
        answers.Add("[������]");
        dialogs.Add(new Dialog("����ϸ�...", answers, EndConversation));

        ProceedConversation();
    }

    public void ProceedConversation()
    {
        speechBubbleText.text = dialogs[dialogIndex].contents;
        GameManager.UI.ShowSelectedButton(dialogs[dialogIndex], parent);
        dialogIndex++;
    }

    public void EndConversation()
    {
        // ù��° �������� isgoable ó��
        GameManager.Game.StartMap();
        GameManager.UI.ShowUI(mapUI);
    }
}