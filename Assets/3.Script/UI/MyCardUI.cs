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
    private RectTransform content; // �ȿ� ������ ������ height�� ��������� ��
    [SerializeField]
    private Transform myCardsParent;

    [SerializeField]
    private Button[] sortButtons;

    /*
    descending order
    ascending order
     */

    // ���������ΰ�??
    private bool[] isAscending = new bool[4];

    private void Awake()
    {
        // onclick ����
        for(int i = 0; i < sortButtons.Length; i++)
        {
            ESortType sortTypeIndex = (ESortType)i;
            sortButtons[i].onClick.AddListener(() => sortType = sortTypeIndex);
            sortButtons[i].onClick.AddListener(() => Sort());
        }
    }

    private void OnEnable()
    {
        // ������������ �ʱ�ȭ
        for(int i = 0; i < isAscending.Length; i++)
            isAscending[i] = false;

        // �ʱ�ȭ
        myCards = new List<BaseCard>();

        // ī�� �־��ֱ�
        foreach (BaseCard card in BattleManager.Instance.Player.myCards)
        {
            BaseCard cloneCard = Instantiate(card, myCardsParent);
            cloneCard.transform.localEulerAngles = Vector3.zero;
            cloneCard.transform.localScale = Vector3.one;
            cloneCard.isBattle = false;
            myCards.Add(cloneCard);
        }

        // ī�� ���� �°� ���� ����
        int cardsRow = (myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);
    }

    private void OnDisable()
    {
        content.sizeDelta = Vector2.up * Screen.height;

        // myCardsParent�� �ڽ� �� �����
        Transform[] children = myCardsParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child != myCardsParent.transform)
                Destroy(child.gameObject);
    }

    public void Sort()
    {
        Debug.Log("�����մϴ�. " + sortType);

        // �ش� ����Ÿ������ �����ϰ� ���� ������ ������ �ֽŽ����� ����
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

        // ���� ������ �ٲ���
        myCards.ForEach(card => card.transform.SetAsLastSibling());
    }
}
