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
        answers.Add("[대화한다]");
        System.Action action = ProceedConversation;
        actions.Add(action);
        dialogs.Add(new Dialog("반갑군...", answers, actions));

        answers = new List<string>();
        actions = new List<System.Action>();
        action = null;
        action += ProceedConversation;
        action += () => battleManager.Player.PlayerRelic.AddRelic(ERelic.NeowLament);
        System.Action action2 = ProceedConversation;
        action2 += () => battleManager.Player.PlayerStat.MaxHp += 7;
        actions.Add(action);
        actions.Add(action2);
        answers.Add("[다음 세 전투를 적들의 체력이 1인 상태로 시작합니다]");
        answers.Add("[최대 체력 +7]");
        dialogs.Add(new Dialog("선택하여라...", answers, actions));

        answers = new List<string>();
        actions = new List<System.Action>();
        answers.Add("[떠난다]");
        action = EndConversation;
        actions.Add(action);
        dialogs.Add(new Dialog("허락하마...", answers, actions));

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
        // 첫번째 스테이지 isgoable 처리
        GameManager.Game.StartMap();
        GameManager.UI.ShowUI(mapUI);
    }
}
