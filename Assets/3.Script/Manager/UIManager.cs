using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private Stack<BaseUI> stackUI = new Stack<BaseUI>();

    private GameObject _root = null;
    private Canvas _rootCanvas = null;

    private Vector2 _originPos;

    public TipUI TipUI { get; private set; }
    public List<Button> SelectedButtons { get; private set; } = new List<Button>();
    public BaseUI CurrentUI
    {
        get
        {
            if (stackUI.Count == 0)
                return null;
            return stackUI.Peek();
        }
    }

    public GameObject Root
    {
        get 
        {
            if(_root == null)
            {
                _root = new GameObject(typeof(UIManager).Name);
                _root.transform.SetParent(GameManager.Instance.transform);
            }
            return _root; 
        }
    }

    public Canvas RootCanvas
    {
        get
        {
            if(_rootCanvas == null)
            {
                _rootCanvas = new GameObject("Canvas").AddComponent<Canvas>();
                _rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                _rootCanvas.transform.SetParent(Root.transform);
            }
            return _rootCanvas;
        }
    }

    public void Init()
    {
        TipUI = Object.Instantiate(Resources.Load<TipUI>("UI/Tip"), RootCanvas.transform);
        for(int i = 0; i < 4; i++)
        {
            SelectedButtons.Add(Object.Instantiate(Resources.Load<Button>("UI/EnableButton"), RootCanvas.transform));
        }

        _originPos = SelectedButtons[0].GetComponent<RectTransform>().anchoredPosition;

        InitUIParent();
        GameManager.Scene.onMoveOtherScene += InitUIParent;
    }

    public void ClearUI()
    {
        stackUI = new Stack<BaseUI>();
    }

    // ui를 보여줍니다.
    public void ShowUI(BaseUI ui)
    {
        if(stackUI.Count > 0)
        {
            stackUI.Peek().gameObject.SetActive(false);
            stackUI.Peek().Hide();
        }
        stackUI.Push(ui);
        if(ui != null)
        {
            stackUI.Peek().gameObject.SetActive(true);
            stackUI.Peek().Show();
            stackUI.Peek().Init();
        }
    }

    // ui를 닫습니다.
    public void PopUI()
    {
        if (stackUI.Count > 0)
        {
            stackUI.Peek().gameObject.SetActive(false);
            stackUI.Peek().Hide();
        }
        stackUI.Pop();
        if (stackUI.Count > 0)
        {
            stackUI.Peek().gameObject.SetActive(true);
            stackUI.Peek().Show();
        }
    }

    public void ShowThisUI(BaseUI ui)
    {
        PopAllUI();
        ShowUI(ui);
    }

    // 모든 UI를 종료합니다.
    public void PopAllUI()
    {
        while(stackUI.Count > 0)
        {
            PopUI();
        }
    }

    // 카드나 범례 누를 때 팁UI 등장하는 함수
    public void ShowTipUI(string title, string content, ETipPos tipPos, Transform parent = null)
    {
        TipUI.gameObject.SetActive(true);
        TipUI.ShowTipUI(title + "\n" + content, tipPos, parent);
    }

    public void ShowTipUI(string title, string content, Vector3 pos, ETipPos tipPos)
    {
        TipUI.gameObject.SetActive(true);
        TipUI.ShowTipUI(title + "\n" + content, pos, tipPos);
    }

    // 나온 팁UI 숨기는 함수
    public void HideTipUI()
    {
        TipUI.gameObject.SetActive(false);
        TipUI.transform.SetParent(RootCanvas.transform);
    }


    public void ShowSelectedButton(Dialog dialog, Transform parent)
    {
        InitSelectedButton();

        Vector2 offset = new Vector2(30, 30);
        float margin = 30f;

        for(int i = 0; i < dialog.Count; i++)
        {
            int index = i;

            SelectedButtons[i].gameObject.SetActive(true);
            SelectedButtons[i].transform.SetParent(parent);
            SelectedButtons[i].transform.SetAsFirstSibling();
            SelectedButtons[i].GetComponentInChildren<Text>().text = dialog.answers[i];
            SelectedButtons[i].onClick.AddListener(() => dialog.onClickButtons[index]());

            SelectedButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(offset.x, offset.y + SelectedButtons[0].GetComponent<RectTransform>().sizeDelta.y * i + margin);
        }
    }

    public void InitSelectedButton()
    {
        SelectedButtons.ForEach(selectedButton => selectedButton.transform.SetParent(RootCanvas.transform));
        SelectedButtons.ForEach(selectedButton => selectedButton.onClick.RemoveAllListeners());
        SelectedButtons.ForEach(selectgedButton => selectgedButton.gameObject.SetActive(false));
        
        // SelectedButtons[0].GetComponent<RectTransform>().anchoredPosition = _originPos;
    }

    // UI 비활성화하고 원래부모로 이동
    public void InitUIParent()
    {
        TipUI.transform.SetParent(RootCanvas.transform);
        TipUI.gameObject.SetActive(false);
        InitSelectedButton();
    }
}
