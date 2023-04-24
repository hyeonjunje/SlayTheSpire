using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private GameScoreUI gameScoreUI;

    [SerializeField]
    private GameObject originBackground, endingBackground, heartComment;
    [SerializeField]
    private Text speechBubbleText;
    [SerializeField]
    private Transform parent;

    private List<Dialog> dialogs;
    private int dialogIndex = 0;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void Awake()
    {
        // 소리 초기화
        GameManager.Sound.StopBgm();
        // 대화 초기화
        dialogs = new List<Dialog>();
        dialogIndex = 0;
        // 배경 초기화
        speechBubbleText.gameObject.SetActive(true);
        originBackground.gameObject.SetActive(false);
        endingBackground.gameObject.SetActive(true);
        // 심장 말풍선 초기화
        heartComment.SetActive(true);
        // UI초기화
        GameManager.UI.InitSelectedButton();
        GameManager.UI.PopAllUI();
        // 플레이어 상태 초기화
        battleManager.Player.PlayerStat.IsBattle(false);

        // 대화
        string str1 = "쿠-쿵 ... 쿠-쿵 ... 쿠-쿵 ...\n" +
            "깊이 고동치는 공포가 방 전체에 걸쳐 느껴집니다...\n" +
            "이것이 첨탑의 <color=red>심장</color> 일까요? 이 모든 악의 근원인 걸까요?";

        string str2 = "당신은 검을 준비합니다...";

        string str3 = "당신은 <color=blue>" + GameManager.Game.totalDamage + "</color> 의 피해를 주었습니다!\n" +
            "심장이 <color=red>꿈틀대며 피를 흘립니다</color>... 하지만 심장은 아직도 뛰고 있습니다. \n" +
            "혼신의 공격이었는데도 부족했던 걸까요?";

        string str4 = "당신은 스스로에게 질문합니다. \"전에 여기 온 적이 있던가?\"\n" +
            "심장은 더욱 더 크게 맥박 치고 당신의 <color=yellow>의식은 점점 흐릿해집니다...</color>";

        string answer1 = "[계속]";
        string answer2 = "[공격]<color=blue>???</color>";
        string answer3 = "[계속]";
        string answer4 = "[수면]";

        List<System.Action> actions = new List<System.Action>();
        actions.Add(ProceedConversation);
        dialogs.Add(new Dialog(str1, answer1, actions));
        actions = new List<System.Action>();
        actions.Add(AttackAndProceedConversation);
        dialogs.Add(new Dialog(str2, answer2, actions));
        actions = new List<System.Action>();
        actions.Add(ProceedConversation);
        dialogs.Add(new Dialog(str3, answer3, actions));
        actions = new List<System.Action>();
        actions.Add(EndConversation);
        dialogs.Add(new Dialog(str4, answer4, actions));

        ProceedConversation();
    }

    public void AttackAndProceedConversation()
    {
        Debug.Log("파바박");

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
        // 심장 없애주고 심장 대화창도 없애주고 배경도 없애주고
        speechBubbleText.gameObject.SetActive(false);
        gameObject.SetActive(false);
        endingBackground.SetActive(false);
        heartComment.SetActive(false);

        // 선택창 없애주기
        GameManager.UI.InitSelectedButton();

        // 플레이어 쓰러지고
        battleManager.Player.PlayerStat.CurrentHp = 0;

        // 점수판 나오기
        gameScoreUI.GameClear();
        GameManager.UI.ShowThisUI(gameScoreUI);
    }


    // 애니메이션 이벤트
    private void PlaySe()
    {
        GameManager.Sound.PlaySE(ESE.Heart);
    }

}
