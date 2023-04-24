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
        // �Ҹ� �ʱ�ȭ
        GameManager.Sound.StopBgm();
        // ��ȭ �ʱ�ȭ
        dialogs = new List<Dialog>();
        dialogIndex = 0;
        // ��� �ʱ�ȭ
        speechBubbleText.gameObject.SetActive(true);
        originBackground.gameObject.SetActive(false);
        endingBackground.gameObject.SetActive(true);
        // ���� ��ǳ�� �ʱ�ȭ
        heartComment.SetActive(true);
        // UI�ʱ�ȭ
        GameManager.UI.InitSelectedButton();
        GameManager.UI.PopAllUI();
        // �÷��̾� ���� �ʱ�ȭ
        battleManager.Player.PlayerStat.IsBattle(false);

        // ��ȭ
        string str1 = "��-�� ... ��-�� ... ��-�� ...\n" +
            "���� ��ġ�� ������ �� ��ü�� ���� �������ϴ�...\n" +
            "�̰��� ÷ž�� <color=red>����</color> �ϱ��? �� ��� ���� �ٿ��� �ɱ��?";

        string str2 = "����� ���� �غ��մϴ�...";

        string str3 = "����� <color=blue>" + GameManager.Game.totalDamage + "</color> �� ���ظ� �־����ϴ�!\n" +
            "������ <color=red>��Ʋ��� �Ǹ� �긳�ϴ�</color>... ������ ������ ������ �ٰ� �ֽ��ϴ�. \n" +
            "ȥ���� �����̾��µ��� �����ߴ� �ɱ��?";

        string str4 = "����� �����ο��� �����մϴ�. \"���� ���� �� ���� �ִ���?\"\n" +
            "������ ���� �� ũ�� �ƹ� ġ�� ����� <color=yellow>�ǽ��� ���� �帴�����ϴ�...</color>";

        string answer1 = "[���]";
        string answer2 = "[����]<color=blue>???</color>";
        string answer3 = "[���]";
        string answer4 = "[����]";

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
        Debug.Log("�Ĺٹ�");

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
        // ���� �����ְ� ���� ��ȭâ�� �����ְ� ��浵 �����ְ�
        speechBubbleText.gameObject.SetActive(false);
        gameObject.SetActive(false);
        endingBackground.SetActive(false);
        heartComment.SetActive(false);

        // ����â �����ֱ�
        GameManager.UI.InitSelectedButton();

        // �÷��̾� ��������
        battleManager.Player.PlayerStat.CurrentHp = 0;

        // ������ ������
        gameScoreUI.GameClear();
        GameManager.UI.ShowThisUI(gameScoreUI);
    }


    // �ִϸ��̼� �̺�Ʈ
    private void PlaySe()
    {
        GameManager.Sound.PlaySE(ESE.Heart);
    }

}
