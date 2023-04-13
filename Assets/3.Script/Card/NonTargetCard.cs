using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonTargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        CardHolder.isDrag = true;

        CardHolder.selectedCard = this;

        // �̵� �� ���� �̵��ϴ� �ڷ�ƾ ����
        ClearCoroutine();

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ī�尡 ���콺 ���󰡰�
        transform.position = eventData.position;
    }

    // ����� ��
    public void OnDrop(PointerEventData eventData)
    {
        CardHolder.isDrag = false;

        // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
        // ��� ���� y���� 300�̻� -> �̴� �ػ󵵿� ���� �ٲٴ� ������ �ʿ�
        if (eventData.position.y > 300f)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // �ƴϸ� ������
        CardHolder.Relocation();
        // �Ұ� �� �ϰ� nulló��
        CardHolder.selectedCard = null;
    }
}
