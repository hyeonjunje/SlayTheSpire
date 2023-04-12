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
        answers.Add("[��ȭ�Ѵ�]");
        dialogs.Add(new Dialog("�ѹ� �� \n �������ٰ�...?", answers, ProceedConversation));

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
        // ù��° �������� isgoable ó��
        GameManager.Game.StartMap();
        GameObject.Find("@Act1Scene").GetComponent<Act1Scene>().ShowMap();
    }
}
