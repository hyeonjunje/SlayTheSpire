using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => _cardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _cardHolder.isDrag = true;

        _cardHolder.selectedCard = this;

        // �̵� �� ���� �̵��ϴ� �ڷ�ƾ ����
        ClearCoroutine();

        transform.SetAsLastSibling();

        _bezierCurve.gameObject.SetActive(true);
        _cardHolder.MoveCenter(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _bezierCurve.p0.position = transform.position;
        _bezierCurve.p2.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _cardHolder.isDrag = false;

        // Ÿ���� ���� ���� ���
        if (BattleManager.Instance.TargetEnemy != null)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // ������ ���� �� nulló��
        BattleManager.Instance.TargetEnemy = null;

        // �Ұ� �� �ϰ� nulló��
        _cardHolder.selectedCard = null;

        // ������ �� ������ � ��Ȱ��ȭ
        _cardHolder.Relocation();
        _bezierCurve.gameObject.SetActive(false);
    }


    protected override bool Use()
    {
        bool isUsable = base.Use();

        if (isUsable)
        {
            BattleManager.Instance.Player.Act();
            BattleManager.Instance.TargetEnemy.Hit(5);
        }

        return isUsable;
    }
}
