using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected Stack<GameObject> _stackUI = new Stack<GameObject>();

    private void Awake()
    {
        Init();
    }

    public abstract void Init();

    public virtual void ShowUI(GameObject go)
    {
        if(_stackUI.Count > 0)
        {
            _stackUI.Peek().SetActive(false);
        }

        _stackUI.Push(go);
        _stackUI.Peek().SetActive(true);
    }

    public virtual void ExitUI()
    {
        if (_stackUI.Count <= 0)
        {
            Debug.Log("UI�� �����ϴ�.");
            return;
        }

        _stackUI.Peek().SetActive(false);
        _stackUI.Pop();

        if (_stackUI.Count > 0)
        {
            _stackUI.Peek().SetActive(true);
        }
    }
}
