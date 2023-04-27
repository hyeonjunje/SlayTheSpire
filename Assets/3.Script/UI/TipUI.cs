using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ETipPos
{
    Up,  // 부모 위에
    Down, // 부모 밑에
    Right, // 부모 오른쪽
    Left, // 부모 왼쪽
    UpRight, // 부모 위 오른쪽,
    UpLeft,  // 부모 위 왼쪽,
    DownRight,  // 부모 밑 오른쪽
    DownLeft,   // 부모 밑 왼쪽
}

public class TipUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _tipBot, _tipMid, _tipTop;    // 나중에 오브젝트 풀링으로 구현하면 좋을듯
    [SerializeField]
    private Transform tipParent; // tipObject들이 들어갈 부모
    [SerializeField]
    private Text tipText; // 팁에 들어갈 내용

    [SerializeField]
    private float _width = 400f; // 너비
    [SerializeField]
    private float _heightInRow = 40f; // 오브젝트 하나당 높이

    private List<GameObject> _tipObjects = new List<GameObject>();
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ShowTipUI(string content, ETipPos tipPos, Transform parent)
    {
        // 초기화 작업
        _tipObjects.ForEach(elem => Destroy(elem));
        _tipObjects = new List<GameObject>();

        // 개행수 + 2
        int rowCount = CheckContent(content) + 2;

        // 개행 수 만큼 크기 맞춰줌
        _rectTransform.sizeDelta = new Vector2(_width, _heightInRow * rowCount);

        // _tipObjects에 생성한 팁 오브젝트들 관리
        _tipObjects.Add(Instantiate(_tipTop, tipParent));
        for(int i = 0; i < rowCount - 2; i++)
        {
            _tipObjects.Add(Instantiate(_tipMid, tipParent));
        }
        _tipObjects.Add(Instantiate(_tipBot, tipParent));

        // 만든 오브젝트들도 크기 맞춰줌
        for(int i = 0; i < _tipObjects.Count; i++)
        {
            _tipObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector3(_width, _heightInRow * rowCount);
        }

        // 문자 입력
        tipText.text = content;

        // 포지셔닝 (ETipPos에 따라 부모 기준으로 tip의 너비, 높이 만큼 이동시킴)
        Positioning(parent, tipPos);
    }

    public void ShowTipUI(string content, Vector3 pos, ETipPos tipPos)
    {
        // 초기화 작업
        _tipObjects.ForEach(elem => Destroy(elem));
        _tipObjects = new List<GameObject>();

        // 개행수 + 2
        int rowCount = CheckContent(content) + 2;

        // 개행 수 만큼 크기 맞춰줌
        _rectTransform.sizeDelta = new Vector2(_width, _heightInRow * rowCount);

        // _tipObjects에 생성한 팁 오브젝트들 관리
        _tipObjects.Add(Instantiate(_tipTop, tipParent));
        for (int i = 0; i < rowCount - 2; i++)
        {
            _tipObjects.Add(Instantiate(_tipMid, tipParent));
        }
        _tipObjects.Add(Instantiate(_tipBot, tipParent));

        // 만든 오브젝트들도 크기 맞춰줌
        for (int i = 0; i < _tipObjects.Count; i++)
        {
            _tipObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector3(_width, _heightInRow * rowCount);
        }

        // 문자 입력
        tipText.text = content;

        Positioning(pos, tipPos);
    }

    private void Positioning(Transform parent, ETipPos tipPos)
    {
        transform.SetParent(parent);

        float width = _rectTransform.sizeDelta.x;
        float height = _rectTransform.sizeDelta.y;

        switch (tipPos)
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
                _rectTransform.anchoredPosition = new Vector2(width / 2, 0);
                break;
            case ETipPos.Left:
                _rectTransform.anchorMin = new Vector2(0f, 0.5f);
                _rectTransform.anchorMax = new Vector2(0f, 0.5f);
                _rectTransform.anchoredPosition = new Vector2(-width / 2, 0);
                break;
            case ETipPos.UpRight:
                _rectTransform.anchorMin = new Vector2(1f, 1f);
                _rectTransform.anchorMax = new Vector2(1f, 1f);
                _rectTransform.anchoredPosition = new Vector2(width / 2, height / 2);
                break;
            case ETipPos.UpLeft:
                _rectTransform.anchorMin = new Vector2(0f, 1f);
                _rectTransform.anchorMax = new Vector2(0f, 1f);
                _rectTransform.anchoredPosition = new Vector2(-width / 2, height / 2);
                break;
            case ETipPos.DownRight:
                _rectTransform.anchorMin = new Vector2(1f, 0f);
                _rectTransform.anchorMax = new Vector2(1f, 0f);
                _rectTransform.anchoredPosition = new Vector2(width / 2, -height / 2);
                break;
            case ETipPos.DownLeft:
                _rectTransform.anchorMin = new Vector2(0f, 0f);
                _rectTransform.anchorMax = new Vector2(0f, 0f);
                _rectTransform.anchoredPosition = new Vector2(-width / 2, -height / 2);
                break;
        }

        // 위치만 바꿔주고 가장 위에 보이게 다시 설정
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        transform.SetAsLastSibling();
    }

    private void Positioning(Vector2 pos, ETipPos tipPos)
    {
        float width = _rectTransform.sizeDelta.x;
        float height = _rectTransform.sizeDelta.y;

        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);



        switch (tipPos)
        {
            case ETipPos.Up:
                transform.position = pos + new Vector2(0, height / 2);
                break;
            case ETipPos.Down:
                transform.position = pos + new Vector2(0, -height / 2);
                break;
            case ETipPos.Right:
                transform.position = pos + new Vector2(width / 2, 0);
                break;
            case ETipPos.Left:
                transform.position = pos + new Vector2(-width / 2, 0);
                break;
            case ETipPos.UpRight:
                transform.position = pos + new Vector2(width / 2, height / 2);
                break;
            case ETipPos.UpLeft:
                transform.position = pos + new Vector2(-width / 2, height / 2);
                break;
            case ETipPos.DownRight:
                transform.position = pos + new Vector2(width / 2, -height / 2);
                break;
            case ETipPos.DownLeft:
                transform.position = pos + new Vector2(-width / 2, -height / 2);
                break;
        }

        // 위치만 바꿔주고 가장 위에 보이게 다시 설정
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        transform.SetAsLastSibling();
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
