using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonTargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;

        // �̵� �� ���� �̵��ϴ� �ڷ�ƾ ����
        ClearCoroutine();

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ī�尡 ���콺 ���󰡰�
        transform.position = Input.mousePosition;
    }

    // ����� ��
    public void OnDrop(PointerEventData eventData)
    {
        // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
        // �ƴϸ� ������

        _isDrag = false;

        _cardHolder.DisplayMyHand();
    }
}
