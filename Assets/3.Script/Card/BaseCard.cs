using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ECardUsage
{
    Battle,   // 배틀
    Check,    // 확인
    Gain,     // 얻기
    Enforce,  // 강화
    DisCard,  // 제거
    Sell,     // 판매
}

public class BaseCard : MonoBehaviour
{
    // 정렬기준
    public int generateNumber;
    public ECardType cardType;
    public int cost;
    public string cardName;

    public bool isEnforce = false;

    // 온클릭 함수
    public Action onClickAction;

    // 상점 시 상인의 손이 따라오게 만들려고 만듬
    public System.Action<Vector3> onPointerEnter;

    [SerializeField]
    private CardController _cardController;
    [SerializeField]
    private BaseCardBuilder _baseCardBuilder;

    private BaseCardStateFactory _baseCardStateFactory;
    private CardHolder _cardHolder;
    private CardData _cardData;   // 카드 데이터

    public CardData CardData => _cardData;
    public BaseCardState CurrentState => _baseCardStateFactory.CurrentState;
    public CardHolder  CardHolder => _cardHolder;
    public CardController CardController => _cardController;
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    // 카드를 생성할 때 이 함수가 실행
    public void Init(CardHolder cardHolder, CardData cardData, CardFrameData cardFrameData, int generateNumber)
    {
        _baseCardStateFactory = new BaseCardStateFactory(this);

        cardData.Init();

        _cardHolder = cardHolder;
        _cardData = cardData;

        _cardController.Init(_cardData.isBezierCurve, this);
        _baseCardBuilder.Init(cardData, cardFrameData, this);

        // 정렬 데이터
        this.generateNumber = generateNumber;
        cardType = _cardData.cardType;
        cost = _cardData.cost;
        cardName = _cardData.cardName;
    }

    public void AddCardTemporary()
    {
        _cardHolder.AddCardTemporary(this);
    }

    public void UseCard()
    {
        if (TryUseCard())
        {
            // 카드의 효과를 사용
            _cardData.UseEffect.ForEach(useEffect => useEffect?.Invoke());


            if(_cardData.isExtinction)
            {
                GameManager.Sound.PlaySE(ESE.CardExhaust);

                // 소멸 카드면 소멸
                _cardHolder.Extinction(this);
            }
            else
            {
                // 카드 버림
                _cardHolder.DiscardCard(this);
            }
        }
    }

    // 카드의 상태를 바꿈
    public void ChangeState(ECardUsage cardUsage)
    {
        _baseCardStateFactory.ChangeState(cardUsage);
    }

    private bool TryUseCard()
    {
        // 코스트 확인, 저주카드 확인, 부상카드 확인, 유물 확인
        if (battleManager.Player.PlayerStat.CurrentOrb >= _cardData.Cost)
        {
            battleManager.Player.PlayerStat.CurrentOrb -= _cardData.Cost;
            return true;
        }
        else
        {
            _cardController.SetActiveRaycast(true);
            return false;
        }
    }

    // 강화모드일 때 카드를 누르면 이 함수가 실행되어야 함
    public void Enforce()
    {
        // 화면 흔들림
        WindowShake.Instance.ShakeWindow();

        // 강화 소리 재생
        GameManager.Sound.PlaySE(ESE.UpgradeCard);

        _cardData.Enforce();

        // 외형이 바껴야 함
        _baseCardBuilder.Enforce(_cardData);

        isEnforce = true;
    }

    public void Discard()
    {
        // 내 카드에서 제거함
        battleManager.Player.myCards.Remove(this);
    }
}
