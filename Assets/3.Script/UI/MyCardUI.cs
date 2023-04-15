using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum ESortType
{
    Recent,
    Type,
    Cost,
    Name,
    Size
}

public class MyCardUI : MonoBehaviour
{
    private List<BaseCard> myCards;
    private ESortType sortType = ESortType.Recent;

    [SerializeField]
    private RectTransform content; // 안에 내용이 많으면 height를 설정해줘야 함
    [SerializeField]
    private Transform myCardsParent;

    [SerializeField]
    private Button[] sortButtons;

    /*
    descending order
    ascending order
     */

    // 오름차순인가??
    private bool[] isAscending = new bool[4];

    private void Awake()
    {
        // onclick 설정
        for(int i = 0; i < sortButtons.Length; i++)
        {
            ESortType sortTypeIndex = (ESortType)i;
            sortButtons[i].onClick.AddListener(() => sortType = sortTypeIndex);
            sortButtons[i].onClick.AddListener(() => Sort());
        }
    }

    private void OnEnable()
    {
        // 내림차순으로 초기화
        for(int i = 0; i < isAscending.Length; i++)
            isAscending[i] = false;

        // 초기화
        myCards = new List<BaseCard>();

        // 카드 넣어주기
        foreach (BaseCard card in BattleManager.Instance.Player.myCards)
        {
            BaseCard cloneCard = Instantiate(card, myCardsParent);
            cloneCard.transform.localEulerAngles = Vector3.zero;
            cloneCard.transform.localScale = Vector3.one;
            cloneCard.isBattle = false;
            myCards.Add(cloneCard);
        }

        // 카드 수에 맞게 높이 조정
        int cardsRow = (myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);
    }

    private void OnDisable()
    {
        content.sizeDelta = Vector2.up * Screen.height;

        // myCardsParent의 자식 다 지우기
        Transform[] children = myCardsParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child != myCardsParent.transform)
                Destroy(child.gameObject);
    }

    public void Sort()
    {
        Debug.Log("정렬합니다. " + sortType);

        // 해당 정렬타입으로 정렬하고 만약 순서가 같으면 최신신으로 정렬
        switch (sortType)
        {
            case ESortType.Recent:
                myCards = myCards.OrderBy(x => x.recentNum).ToList();
                break;
            case ESortType.Type:
                myCards = myCards.OrderBy(x => x.cardName).ThenBy(x => x.recentNum).ToList();
                break;
            case ESortType.Cost:
                myCards = myCards.OrderBy(x => x.cost).ThenBy(x => x.recentNum).ToList();
                break;
            case ESortType.Name:
                myCards = myCards.OrderBy(x => x.cardName).ThenBy(x => x.recentNum).ToList();
                break;
        }

        // 정렬 순서로 바꿔줌
        myCards.ForEach(card => card.transform.SetAsLastSibling());
    }
}
