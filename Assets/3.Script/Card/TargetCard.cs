using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => GameManager.Game.CardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;

        // �̵� �� ���� �̵��ϴ� �ڷ�ƾ ����
        ClearCoroutine();

        transform.SetAsLastSibling();

        _bezierCurve.gameObject.SetActive(true);
        _bezierCurve.p0.position = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ī�尡 ���콺 ���󰡰�
        _bezierCurve.p2.position = eventData.position;
    }

    // ����� �� �ִ� ���� ����� ��
    public void OnDrop(PointerEventData eventData)
    {
        // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
        // �ƴϸ� ������

        _isDrag = false;

        _cardHolder.DisplayMyHand();
        _bezierCurve.gameObject.SetActive(false);
    }

    // �Ѵ� 
    public void OnEndDrag(PointerEventData eventData)
    {
        // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
        // �ƴϸ� ������

        _isDrag = false;

        _cardHolder.DisplayMyHand();
        _bezierCurve.gameObject.SetActive(false);
    }
}
