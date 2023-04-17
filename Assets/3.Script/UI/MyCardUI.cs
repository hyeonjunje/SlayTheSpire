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

    [SerializeField]
    private Image[] sortDirImage;

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
            int index = i;
            sortButtons[i].onClick.AddListener(() => sortType = sortTypeIndex);
            sortButtons[i].onClick.AddListener(() => Sort(index));
        }
    }

    private void OnEnable()
    {
        // 내림차순으로 초기화
        for(int i = 0; i < isAscending.Length; i++)
        {
            isAscending[i] = false;
            sortDirImage[i].transform.localScale = Vector3.one;
        }

        // 초기화
        myCards = new List<BaseCard>();

        // 카드 넣어주기
        foreach (BaseCard card in BattleManager.Instance.Player.myCards)
        {
            BaseCard cloneCard = Instantiate(card, myCardsParent);

            cloneCard.transform.localEulerAngles = Vector3.zero;
            cloneCard.transform.localScale = Vector3.one;
            
            // 나중에 고쳐야 함
            cloneCard.EndBattle();

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

    /// <summary>
    /// 카드를 어떤 기준으로 정렬해줌
    /// </summary>
    /// <param name="isAscending">오름차순인가? </param>
    public void Sort(int index)
    {
        Debug.Log("정렬합니다. " + sortType);

        for(int i = 0; i < sortButtons.Length; i++)
        {
            foreach (Image childImage in sortButtons[i].GetComponentsInChildren<Image>())
            {
                childImage.color = i == index ? Color.yellow :  Color.white;
            }
        }

        // 해당 정렬타입으로 정렬하고 만약 순서가 같으면 최신신으로 정렬
        switch (sortType)
        {
            case ESortType.Recent:
                myCards = myCards.OrderBy(x => x.generateNumber).ToList();
                break;
            case ESortType.Type:
                myCards = myCards.OrderBy(x => x.cardType).ThenBy(x => x.generateNumber).ToList();
                break;
            case ESortType.Cost:
                myCards = myCards.OrderBy(x => x.cost).ThenBy(x => x.generateNumber).ToList();
                break;
            case ESortType.Name:
                myCards = myCards.OrderBy(x => x.cardName).ThenBy(x => x.generateNumber).ToList();
                break;
        }

        if(isAscending[index])
        {
            // 정렬 순서로 바꿔줌
            myCards.ForEach(card => card.transform.SetAsLastSibling());
            isAscending[index] = false;
        }
        else
        {
            isAscending[index] = true;
        }
        sortDirImage[index].transform.localScale = new Vector3(sortDirImage[index].transform.localScale.x, 
            sortDirImage[index].transform.localScale.y * -1, sortDirImage[index].transform.localScale.z);
    }
}
