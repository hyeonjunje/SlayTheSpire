using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private CanvasGroup _actInfo;

    [SerializeField] private Player _player;
    [SerializeField] private CardHolder _cardHolder;

    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _myCard;

    [SerializeField] private Neow _neow;

    private Vector3 _exitButtonOriginPos;
    private Coroutine _coAppearExitButton;
    private Coroutine _coDisappearExitButton;

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
        _player.AddCard(cardGenerator.GenerateCard(1));
        _player.AddCard(cardGenerator.GenerateCard(2));
        _player.AddCard(cardGenerator.GenerateCard(2));
        _player.AddCard(cardGenerator.GenerateCard(2));
        _player.AddCard(cardGenerator.GenerateCard(2));
        _player.AddCard(cardGenerator.GenerateCard(2));

        // ĳ���͵� ����������

        _exitButtonOriginPos = _exitButton.transform.position;
        _exitButton.onClick.AddListener(() => ExitUI());
    }

    public override void ShowUI(GameObject go)
    {
        _stackUI.Clear();
        
        base.ShowUI(go);

        if (_coAppearExitButton != null)
            StopCoroutine(_coAppearExitButton);
        _coAppearExitButton = StartCoroutine(CoAppearExitButton());
    }

    public override void ExitUI()
    {
        base.ExitUI();

        if (_coDisappearExitButton != null)
            StopCoroutine(_coDisappearExitButton);
        _coDisappearExitButton = StartCoroutine(CoDisAppearExitButton());
    }

    public void ShowMap()
    {
        ShowUI(_map);
    }

    public void ShowMyCard()
    {
        ShowUI(_myCard);
    }

    public void ShowSetting()
    {

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

    private IEnumerator CoAppearExitButton()
    {
        float timeElapsed = 0;
        RectTransform exitButtonRectTransform = _exitButton.GetComponent<RectTransform>();
        while (true)
        {
            exitButtonRectTransform.position = new Vector3(Mathf.Lerp(_exitButtonOriginPos.x, 0, timeElapsed / 0.2f), _exitButtonOriginPos.y, 0);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 0.2f)
                break;
            yield return null;
        }
    }

    private IEnumerator CoDisAppearExitButton()
    {
        float timeElapsed = 0;
        RectTransform exitButtonRectTransform = _exitButton.GetComponent<RectTransform>();
        while (true)
        {
            exitButtonRectTransform.position = new Vector3(Mathf.Lerp(0, _exitButtonOriginPos.x, timeElapsed / 0.2f), _exitButtonOriginPos.y, 0);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 0.2f)
                break;
            yield return null;
        }
    }
}
