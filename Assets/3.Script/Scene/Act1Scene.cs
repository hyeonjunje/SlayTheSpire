using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene : BaseScene
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private Button _exitButton;


    private Vector3 _exitButtonOriginPos;
    private Coroutine _coAppearExitButton;
    private Coroutine _coDisappearExitButton;

    public override void Init()
    {
        base.Init();

        // 씬 bgm 실행
        GameManager.Sound.PlayBGM(EBGM.Level1);

        // 맵 생성
        _mapGenerator.GenerateMap();
        // 만든 맵 데이터 넘겨주기
        _mapGenerator.SetMapArrayToGameManager();

        // 시작 시 맵 만들기 (보여주는 건 x)
        // 1막 태초 코루틴으로 보여주기

        // 카드도 생성해야 하지
        // 캐릭터도 만들어야하지

        _exitButtonOriginPos = _exitButton.transform.position;
        _exitButton.onClick.AddListener(() => ExitUI());
    }

    public override void ShowUI(GameObject go)
    {
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
