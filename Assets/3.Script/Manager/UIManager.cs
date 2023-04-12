using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private GameObject _root = null;
    private Canvas _rootCanvas = null;

    public TipUI TipUI { get; private set; }
    public List<Button> SelectedButtons { get; private set; } = new List<Button>();

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

        InitUIParent();
        GameManager.Scene.onMoveOtherScene += InitUIParent;
    }

    // ī�峪 ���� ���� �� ��UI �����ϴ� �Լ�
    public void ShowTipUI(string title, string content, ETipPos tipPos, Transform parent = null)
    {
        TipUI.gameObject.SetActive(true);
        TipUI.ShowTipUI(title + "\n" + content, tipPos, parent);
    }

    // ���� ��UI ����� �Լ�
    public void HideTipUI()
    {
        TipUI.gameObject.SetActive(false);
        TipUI.transform.SetParent(RootCanvas.transform);
    }

    // UI ��Ȱ��ȭ�ϰ� �����θ�� �̵�
    public void InitUIParent()
    {
        TipUI.transform.SetParent(RootCanvas.transform);
        TipUI.gameObject.SetActive(false);
        SelectedButtons.ForEach(selectedButton => selectedButton.transform.SetParent(RootCanvas.transform));
        SelectedButtons.ForEach(selectgedButton => selectgedButton.gameObject.SetActive(false));
    }
}
