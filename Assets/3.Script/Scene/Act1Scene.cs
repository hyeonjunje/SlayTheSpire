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

        // �� bgm ����
        GameManager.Sound.PlayBGM(EBGM.Level1);

        // �� ����
        _mapGenerator.GenerateMap();
        // ���� �� ������ �Ѱ��ֱ�
        _mapGenerator.SetMapArrayToGameManager();

        // ���� �� �� ����� (�����ִ� �� x)
        // 1�� ���� �ڷ�ƾ���� �����ֱ�

        // ī�嵵 �����ؾ� ����
        // ĳ���͵� ����������

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
