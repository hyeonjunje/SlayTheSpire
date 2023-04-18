using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    private Vector3 _exitButtonOriginPos;

    [SerializeField]
    private bool isFirst;  // 무조건 처음이여야 하는 UI 

    public virtual void Show()
    {
        if(isFirst)
        {
        }
        else
        {
            _exitButton.gameObject.SetActive(true);

            /*StopAllCoroutines();
            StartCoroutine(CoAppearExitButton());*/
        }
    }

    public virtual void Hide()
    {
        if(isFirst)
        {
        }
        else
        {
            _exitButton.gameObject.SetActive(false);

            /*StopAllCoroutines();
            StartCoroutine(CoDisAppearExitButton());*/
        }
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
