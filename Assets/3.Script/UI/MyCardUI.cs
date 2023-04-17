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

    [SerializeField]
    private Image[] sortDirImage;

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
            int index = i;
            sortButtons[i].onClick.AddListener(() => sortType = sortTypeIndex);
            sortButtons[i].onClick.AddListener(() => Sort(index));
        }
    }

    private void OnEnable()
    {
        // ������������ �ʱ�ȭ
        for(int i = 0; i < isAscending.Length; i++)
        {
            isAscending[i] = false;
            sortDirImage[i].transform.localScale = Vector3.one;
        }

        // �ʱ�ȭ
        myCards = new List<BaseCard>();

        // ī�� �־��ֱ�
        foreach (BaseCard card in BattleManager.Instance.Player.myCards)
        {
            BaseCard cloneCard = Instantiate(card, myCardsParent);

            cloneCard.transform.localEulerAngles = Vector3.zero;
            cloneCard.transform.localScale = Vector3.one;
            
            // ���߿� ���ľ� ��
            cloneCard.EndBattle();

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
            // ���� ������ �ٲ���
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
