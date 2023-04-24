using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] protected Button _exitButton;
    private Vector3 _exitButtonOriginPos;

    [SerializeField]
    private bool isFirst;  // ������ ó���̿��� �ϴ� UI 
    [SerializeField]
    private bool irrevocable;  // �ڷ� �� �� ���� UI (�̺�Ʈ���� �� ī�带 ��ȭ�ϰų� �����ؾ��ϴ� ��)

    public virtual void Show()
    {
        if(isFirst || irrevocable)
        {
        }
        else
        {
            _exitButton.gameObject.SetActive(true);
        }
    }

    public virtual void Hide()
    {
        if(isFirst || irrevocable)
        {
        }
        else
        {
            _exitButton.gameObject.SetActive(false);

            /*StopAllCoroutines();
            StartCoroutine(CoDisAppearExitButton());*/
        }
    }

    public virtual void Init()
    {

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
