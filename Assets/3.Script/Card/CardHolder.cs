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
    private Transform _cardDeckTransform; // 카드 덱 위치
    [SerializeField]
    private Transform _cardCemetryTransform; // 카드 묘지 위치

    [SerializeField]
    private List<BaseCard> _cardDeck;  // 카드 덱
    [SerializeField]
    private List<BaseCard> _cardHands; // 카드 패
    [SerializeField]
    private List<BaseCard> _cardCemetry; // 카드 묘지
    [SerializeField]
    private List<BaseCard> _cardExtinction; // 카드 소멸

    [SerializeField]
    private BaseCard _selectedCard;  // 현재 집은 카드

    [SerializeField]
    private float offsetAngle;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float distance;

    void Update()
    {
        // 드로우 테스트
        if (Input.GetKeyDown(KeyCode.A))
        {
            DrawCard();
        }


        // 핸드 다 버리기 테스트
        if(Input.GetKeyDown(KeyCode.S))
        {
            GoToCemetryAllHands();
        }
    }

    // 전투 시작시 플레이어의 카드를 카드 홀더에 넣는다.
    public void InitCardHolder(List<BaseCard> cardDeck)
    {
        _cardDeck = cardDeck;

        // Todo : 5말고 player의 카드 드로우 수만큼 뽑아야 함
        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }
    }

    // 카드 드로우
    public void DrawCard()
    {
        // 덱에 카드가 없으면 묘지에 있는 카드를 덱에 넣고 섞어준다.
        if (_cardDeck.Count == 0)
        {
            ReturnToDeck();
            ShuffleDeck();
        }

        // 덱의 맨 위 카드를 패에 넣어준다.
        _cardHands.Add(_cardDeck[_cardDeck.Count - 1]);
        _cardDeck.RemoveAt(_cardDeck.Count - 1);

        DisplayMyHand();

        // 뽑은 카드는 제일 앞으로
        _cardHands[_cardHands.Count - 1].transform.SetAsFirstSibling();
    }

    // 패에 있는 모든 카드 버리기
    public void GoToCemetryAllHands()
    {
        MoveAllHandsToCemetry();

        while (_cardHands.Count != 0)
        {
            _cardCemetry.Add(_cardHands[_cardHands.Count - 1]);
            _cardHands.RemoveAt(_cardHands.Count - 1);
        }
    }

    // 덱의 카드를 다 쓰면 묘지에 있는 카드가 덱에 들어감
    public void ReturnToDeck()
    {
        // 카드 transform 초기화
        for(int i = 0; i < _cardCemetry.Count; i++)
        {
            _cardCemetry[i].transform.localPosition = _cardDeckTransform.localPosition;
            _cardCemetry[i].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            _cardCemetry[i].transform.localScale = Vector3.zero;
        }

        // 카드를 묘지에서 덱으로 이동
        while (_cardCemetry.Count != 0)
        {
            _cardDeck.Add(_cardCemetry[_cardCemetry.Count - 1]);
            _cardCemetry.RemoveAt(_cardCemetry.Count - 1);
        }
    }

    // 덱을 셔플해준다.
    public void ShuffleDeck()
    {
        Util.ShuffleList(_cardDeck);
    }

    // 핸드 위치 조정
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

        // 카드 관리
        for (int i = 0; i < _cardHands.Count; i++)
        {
            float theta = startTheta + angle * i;
            Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0) * distance;
            Vector3 targetRot = new Vector3(0f, 0f, theta - 90);
            Vector3 targetScl = Vector3.one;

            _cardHands[i].MoveCard(targetPos, targetRot, targetScl);
        }
    }

    // 핸드에서 묘지로 카드 이동
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
