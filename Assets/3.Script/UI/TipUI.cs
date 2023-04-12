using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ETipPos
{
    Up,  // �θ� ����
    Down, // �θ� �ؿ�
    Right, // �θ� ������
    Left, // �θ� ����
}

public class TipUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _tipBot, _tipMid, _tipTop;    // ���߿� ������Ʈ Ǯ������ �����ϸ� ������
    [SerializeField]
    private Transform tipParent; // tipObject���� �� �θ�
    [SerializeField]
    private Text tipText; // ���� �� ����

    [SerializeField]
    private float _width = 400f; // �ʺ�
    [SerializeField]
    private float _heightInRow = 40f; // ������Ʈ �ϳ��� ����

    private List<GameObject> _tipObjects = new List<GameObject>();
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ShowTipUI(string content, ETipPos tipPos, Transform parent)
    {
        // �ʱ�ȭ �۾�
        _tipObjects.ForEach(elem => Destroy(elem));
        _tipObjects = new List<GameObject>();

        // ����� + 2
        int rowCount = CheckContent(content) + 2;

        // ���� �� ��ŭ ũ�� ������
        _rectTransform.sizeDelta = new Vector2(_width, _heightInRow * rowCount);

        // _tipObjects�� ������ �� ������Ʈ�� ����
        _tipObjects.Add(Instantiate(_tipTop, tipParent));
        for(int i = 0; i < rowCount - 2; i++)
        {
            _tipObjects.Add(Instantiate(_tipMid, tipParent));
        }
        _tipObjects.Add(Instantiate(_tipBot, tipParent));

        // ���� ������Ʈ�鵵 ũ�� ������
        for(int i = 0; i < _tipObjects.Count; i++)
        {
            _tipObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector3(_width, _heightInRow * rowCount);
        }

        // ���� �Է�
        tipText.text = content;

        // �����Ŵ� (ETipPos�� ���� �θ� �������� tip�� �ʺ�, ���� ��ŭ �̵���Ŵ)
        Positioning(parent, tipPos);
    }

    private void Positioning(Transform parent, ETipPos tipPos)
    {
        transform.SetParent(parent);

        float width = _rectTransform.sizeDelta.x;
        float height = _rectTransform.sizeDelta.y;

        switch(tipPos)
        {
            case ETipPos.Up:
                _rectTransform.anchorMin = new Vector2(0.5f, 1f);
                _rectTransform.anchorMax = new Vector2(0.5f, 1f);
                _rectTransform.anchoredPosition = new Vector2(0, height / 2);
                break;
            case ETipPos.Down:
                _rectTransform.anchorMin = new Vector2(0.5f, 0f);
                _rectTransform.anchorMax = new Vector2(0.5f, 0f);
                _rectTransform.anchoredPosition = new Vector2(0, -height / 2);
                break;
            case ETipPos.Right:
                _rectTransform.anchorMin = new Vector2(1f, 0.5f);
                _rectTransform.anchorMax = new Vector2(1f, 0.5f);
                _rectTransform.anchoredPosition = new Vector2(width, 0);
                break;
            case ETipPos.Left:
                _rectTransform.anchorMin = new Vector2(0f, 0.5f);
                _rectTransform.anchorMax = new Vector2(0f, 0.5f);
                _rectTransform.anchoredPosition = new Vector2(-width, 0);
                break;
        }
    }

    private int CheckContent(string content)
    {
        int rowCount = 1;

        for(int i = 0; i < content.Length; i++)
        {
            if (content[i] == '\n')
                rowCount++;
        }

        return rowCount;
    }
}