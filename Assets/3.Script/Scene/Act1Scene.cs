using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private CanvasGroup _actInfo;

    [SerializeField] private Player _player;
    [SerializeField] private CardHolder _cardHolder;
    [SerializeField] private GameScoreUI _gameScoreUI;
    [SerializeField] private GameObject _background;

    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _myCard;

    [SerializeField] private Neow _neow;

    private MapGenerator _mapGenerator;

    private CardGenerator _cardGenerator => ServiceLocator.Instance.GetService<CardGenerator>();

    public override void Init()
    {
        base.Init();

        _player.Init();

        _player.onDead = null;
        _player.onDead += (() => _background.SetActive(false));
        _player.onDead += (() => GameManager.UI.ShowThisUI(_gameScoreUI));

        _neow.gameObject.SetActive(true);

        // �� bgm ����
        StartCoroutine(GameManager.Sound.FadeInOutAudioSource(EBGM.Level1));
        // GameManager.Sound.PlayBGM(EBGM.Level1);

        // ���� �� �� ����� (�����ִ� �� x)
        // �� ���� && �� ������ �Ѱ��ֱ�
        _mapGenerator = FindObjectOfType<MapGenerator>();
        GameManager.Game.SetMapArray(_mapGenerator.GenerateMap());

        // 1�� ���� �ڷ�ƾ���� �����ֱ�
        StartCoroutine(CoAppearActInfo());

        // ī�嵵 �����ؾ� ����
        _player.AddCard(_cardGenerator.GenerateCard(1));
        _player.AddCard(_cardGenerator.GenerateCard(1));
        _player.AddCard(_cardGenerator.GenerateCard(1));
        _player.AddCard(_cardGenerator.GenerateCard(1));
        _player.AddCard(_cardGenerator.GenerateCard(1));   // Ÿ��
        _player.AddCard(_cardGenerator.GenerateCard(17));  // ����
        _player.AddCard(_cardGenerator.GenerateCard(17));
        _player.AddCard(_cardGenerator.GenerateCard(17));
        _player.AddCard(_cardGenerator.GenerateCard(17));
        _player.AddCard(_cardGenerator.GenerateCard(17));
        _player.AddCard(_cardGenerator.GenerateCard(2));  // ��Ÿ
    }

    // ��ư�� ��Ŭ��
    public void GoMainMenu()
    {
        GameManager.Scene.LoadScene(ESceneName.Title);
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
