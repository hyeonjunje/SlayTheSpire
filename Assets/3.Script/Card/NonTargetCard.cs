/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonTargetCard : BaseCard, IBeginDragHandler, IDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        _cardHolder.isDrag = true;

        _cardHolder.selectedCard = this;

        // �̵� �� ���� �̵��ϴ� �ڷ�ƾ ����
        ClearCoroutine();

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        // ī�尡 ���콺 ���󰡰�
        transform.position = eventData.position;
    }

    // ����� ��
    public void OnDrop(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        _cardHolder.isDrag = false;

        // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
        // ��� ���� y���� 300�̻� -> �̴� �ػ󵵿� ���� �ٲٴ� ������ �ʿ�
        if (eventData.position.y > 300f)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // �ƴϸ� ������
        _cardHolder.Relocation();
        // �Ұ� �� �ϰ� nulló��
        _cardHolder.selectedCard = null;
    }

    protected override bool Use()
    {
        bool isUsable = base.Use();

        if(isUsable)
        {
            BattleManager.Instance.Player.ShieldAmount += 5;
        }

        return isUsable;
    }
}
*/