using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECardScale
{
    bigger,
    smaller,
    same,
    size,
}

public class CardHolder : MonoBehaviour
{
    [SerializeField]
    private Transform _cardDeckTransform; // ī�� �� ��ġ
    [SerializeField]
    private Transform _cardCemetryTransform; // ī�� ���� ��ġ

    [SerializeField]
    private List<BaseCard> _cardDeck;  // ī�� ��
    [SerializeField]
    private List<BaseCard> _cardHands; // ī�� ��
    [SerializeField]
    private List<BaseCard> _cardCemetry; // ī�� ����
    [SerializeField]
    private List<BaseCard> _cardExtinction; // ī�� �Ҹ�

    [SerializeField]
    private BaseCard _selectedCard;  // ���� ���� ī��

    [SerializeField]
    private float offsetAngle;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float distance;

    void Update()
    {
        // ��ο� �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.A))
        {
            DrawCard();
        }


        // �ڵ� �� ������ �׽�Ʈ
        if(Input.GetKeyDown(KeyCode.S))
        {
            GoToCemetryAllHands();
        }
    }

    // ���� ���۽� �÷��̾��� ī�带 ī�� Ȧ���� �ִ´�.
    public void InitCardHolder(List<BaseCard> cardDeck)
    {
        _cardDeck = cardDeck;

        // Todo : 5���� player�� ī�� ��ο� ����ŭ �̾ƾ� ��
        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }
    }

    // ī�� ��ο�
    public void DrawCard()
    {
        // ���� ī�尡 ������ ������ �ִ� ī�带 ���� �ְ� �����ش�.
        if (_cardDeck.Count == 0)
        {
            ReturnToDeck();
            ShuffleDeck();
        }

        // ���� �� �� ī�带 �п� �־��ش�.
        _cardHands.Add(_cardDeck[_cardDeck.Count - 1]);
        _cardDeck.RemoveAt(_cardDeck.Count - 1);

        DisplayMyHand();

        // ���� ī��� ���� ������
        _cardHands[_cardHands.Count - 1].transform.SetAsFirstSibling();
    }

    // �п� �ִ� ��� ī�� ������
    public void GoToCemetryAllHands()
    {
        MoveAllHandsToCemetry();

        while (_cardHands.Count != 0)
        {
            _cardCemetry.Add(_cardHands[_cardHands.Count - 1]);
            _cardHands.RemoveAt(_cardHands.Count - 1);
        }
    }

    // ���� ī�带 �� ���� ������ �ִ� ī�尡 ���� ��
    public void ReturnToDeck()
    {
        // ī�� transform �ʱ�ȭ
        for(int i = 0; i < _cardCemetry.Count; i++)
        {
            _cardCemetry[i].transform.localPosition = _cardDeckTransform.localPosition;
            _cardCemetry[i].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            _cardCemetry[i].transform.localScale = Vector3.zero;
        }

        // ī�带 �������� ������ �̵�
        while (_cardCemetry.Count != 0)
        {
            _cardDeck.Add(_cardCemetry[_cardCemetry.Count - 1]);
            _cardCemetry.RemoveAt(_cardCemetry.Count - 1);
        }
    }

    // ���� �������ش�.
    public void ShuffleDeck()
    {
        Util.ShuffleList(_cardDeck);
    }

    // �ڵ� ��ġ ����
    private void DisplayMyHand()
    {
        float startTheta = 0;
        if (_cardHands.Count % 2 == 0)
        {
            startTheta -= _cardHands.Count / 2 * angle - (angle / 2) - 90;
        }
        else
        {
            startTheta -= (_cardHands.Count / 2) * angle - 90;
        }

        // ī�� ����
        for (int i = 0; i < _cardHands.Count; i++)
        {
            float theta = startTheta + angle * i;
            Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0) * distance;
            Vector3 targetRot = new Vector3(0f, 0f, theta - 90);
            Vector3 targetScl = Vector3.one;

            _cardHands[i].MoveCard(targetPos, targetRot, targetScl);
        }
    }

    // �ڵ忡�� ������ ī�� �̵�
    private void MoveAllHandsToCemetry()
    {
        for (int i = 0; i < _cardHands.Count; i++)
        {
            Vector3 targetPos = _cardCemetryTransform.localPosition;
            Vector3 targetRot = new Vector3(0f, 0f, 0f);
            Vector3 targetScl = Vector3.zero;

            _cardHands[i].MoveCard(targetPos, targetRot, targetScl);
        }
    }
}
