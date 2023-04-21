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

public class MyCardUI : BaseUI
{
    private List<BaseCard> myCards;
    private ESortType sortType = ESortType.Recent;

    [SerializeField]
    private RectTransform content; // �ȿ� ������ ������ height�� ��������� ��
    [SerializeField]
    private Transform myCardsParent;

    [SerializeField]
    private Button[] sortButtons;

    [SerializeField]
    private Image[] sortDirImage;

    // ���������ΰ�??
    private bool[] isAscending = new bool[4];

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    private void Awake()
    {
        // onclick ����
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
        myCards = new List<BaseCard>();
        myCards = battleManager.Player.myCards;
        for(int i = 0; i < myCards.Count; i++)
        {
            myCards[i].ChangeState(ECardUsage.Check);
            myCards[i].transform.SetParent(myCardsParent);

            myCards[i].transform.localEulerAngles = Vector3.zero;
            myCards[i].transform.localScale = Vector3.one;
        }

        // ������������ �ʱ�ȭ
        for (int i = 0; i < isAscending.Length; i++)
        {
            isAscending[i] = false;
            sortDirImage[i].transform.localScale = Vector3.one;
        }

        // ī�� ���� �°� ���� ����
        int cardsRow = (myCards.Count - 1) / 5;
        content.sizeDelta += new Vector2(0, (cardsRow - 1) * 400f);

        Sort((int)ESortType.Recent);
    }

    private void OnDisable()
    {
        content.sizeDelta = Vector2.up * Screen.height;
    }

    /// <summary>
    /// ī�带 � �������� ��������
    /// </summary>
    /// <param name="isAscending">���������ΰ�? </param>
    public void Sort(int index)
    {
        Debug.Log("�����մϴ�. " + sortType);

        for(int i = 0; i < sortButtons.Length; i++)
        {
            foreach (Image childImage in sortButtons[i].GetComponentsInChildren<Image>())
            {
                childImage.color = i == index ? Color.yellow :  Color.white;
            }
        }

        // �ش� ����Ÿ������ �����ϰ� ���� ������ ������ �ֽŽ����� ����
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
            myCards.ForEach(card => card.transform.SetAsLastSibling());
            isAscending[index] = false;
        }
        else
        {
            myCards.ForEach(card => card.transform.SetAsFirstSibling());
            isAscending[index] = true;
        }
        sortDirImage[index].transform.localScale = new Vector3(sortDirImage[index].transform.localScale.x, 
            sortDirImage[index].transform.localScale.y * -1, sortDirImage[index].transform.localScale.z);
    }
}
