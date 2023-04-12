using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private Button _exitButton;
    [SerializeField] private CanvasGroup _actInfo;

    [SerializeField] private GameObject _map;

    private Vector3 _exitButtonOriginPos;
    private Coroutine _coAppearExitButton;
    private Coroutine _coDisappearExitButton;

    public override void Init()
    {
        base.Init();

        // 씬 bgm 실행
        GameManager.Sound.PlayBGM(EBGM.Level1);

        // 시작 시 맵 만들기 (보여주는 건 x)
        // 맵 생성 && 맵 데이터 넘겨주기
        GameManager.Game.SetMapArray(_mapGenerator.GenerateMap());

        // 1막 태초 코루틴으로 보여주기
        StartCoroutine(CoAppearActInfo());

        // 카드도 생성해야 하지
        // 캐릭터도 만들어야하지

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

    private IEnumerator CoAppearActInfo()
    {
        _actInfo.gameObject.SetActive(true);

        // 1.5초간 등장
        float timeElapsed = 0;
        while (true)
        {
            _actInfo.alpha = Mathf.Lerp(0, 1, timeElapsed / 1.5f);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.5f)
                break;
            yield return null;
        }

        // 1초간 유지
        yield return new WaitForSeconds(1f);

        // 1.5초간 사라짐
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
