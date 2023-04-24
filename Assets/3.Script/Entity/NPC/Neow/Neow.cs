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

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    private void Awake()
    {
        dialogIndex = 0;

        List<string> answers = new List<string>();
        List<System.Action> actions = new List<System.Action>();
        answers.Add("[��ȭ�Ѵ�]");
        System.Action action = ProceedConversation;
        actions.Add(action);
        dialogs.Add(new Dialog("�ݰ���...", answers, actions));

        answers = new List<string>();
        actions = new List<System.Action>();
        action = null;
        action += ProceedConversation;
        action += () => battleManager.Player.PlayerRelic.AddRelic(ERelic.NeowLament);
        System.Action action2 = ProceedConversation;
        action2 += () => battleManager.Player.PlayerStat.MaxHp += 7;
        actions.Add(action);
        actions.Add(action2);
        answers.Add("[���� �� ������ ������ ü���� 1�� ���·� �����մϴ�]");
        answers.Add("[�ִ� ü�� +7]");
        dialogs.Add(new Dialog("�����Ͽ���...", answers, actions));

        answers = new List<string>();
        actions = new List<System.Action>();
        answers.Add("[������]");
        action = EndConversation;
        actions.Add(action);
        dialogs.Add(new Dialog("����ϸ�...", answers, actions));

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
