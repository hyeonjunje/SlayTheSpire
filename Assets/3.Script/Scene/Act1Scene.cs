using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private CanvasGroup _actInfo;

    [SerializeField] private Player _player;
    [SerializeField] private CardHolder _cardHolder;

    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _myCard;

    [SerializeField] private Neow _neow;

    private CardGenerator cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();
    private MapGenerator mapGenerator => ServiceLocator.Instance.GetService<MapGenerator>();

    public override void Init()
    {
        base.Init();

        _neow.gameObject.SetActive(true);

        // �� bgm ����
        GameManager.Sound.PlayBGM(EBGM.Level1);

        // ���� �� �� ����� (�����ִ� �� x)
        // �� ���� && �� ������ �Ѱ��ֱ�
        GameManager.Game.SetMapArray(mapGenerator.GenerateMap());

        // 1�� ���� �ڷ�ƾ���� �����ֱ�
        StartCoroutine(CoAppearActInfo());

        // ī�嵵 �����ؾ� ����
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(1));   // Ÿ��
        _player.AddCard(cardGenerator.GenerateCard(33));  // ����
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));
        _player.AddCard(cardGenerator.GenerateCard(33));

        // ĳ���͵� ����������
    }


    public void PopUI()
    {
        GameManager.UI.PopUI();
    }

    public void ShowUI(BaseUI ui)
    {
        GameManager.UI.ShowUI(ui);
    }



    private IEnumerator CoAppearActInfo()
    {
        _actInfo.gameObject.SetActive(true);

        // 1.5�ʰ� ����
        float timeElapsed = 0;
        while (true)
        {
            _actInfo.alpha = Mathf.Lerp(0, 1, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.5f)
                break;
            yield return null;
        }

        // 1�ʰ� ����
        yield return new WaitForSeconds(1f);

        // 1.5�ʰ� �����
        timeElapsed = 0;
        while (true)
        {
            _actInfo.alpha = Mathf.Lerp(1, 0, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.5f)
                break;
            yield return null;
        }

        _actInfo.gameObject.SetActive(false);
    }
}
