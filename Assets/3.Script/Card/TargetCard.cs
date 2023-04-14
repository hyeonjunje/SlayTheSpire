using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetCard : BaseCard, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private BezierCurve _bezierCurve => CardHolder.BezierCurve;

    public void OnBeginDrag(PointerEventData eventData)
    {
        CardHolder.isDrag = true;

        CardHolder.selectedCard = this;

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


    public void OnEndDrag(PointerEventData eventData)
    {
        CardHolder.isDrag = false;

        // Ÿ���� ���� ���� ���
        if (BattleManager.Instance.TargetEnemy != null)
        {
            SetActiveRaycast(false);
            UseCardMove();
        }

        // ������ ���� �� nulló��
        BattleManager.Instance.TargetEnemy = null;

        // �Ұ� �� �ϰ� nulló��
        CardHolder.selectedCard = null;

        // ������ �� ������ � ��Ȱ��ȭ
        CardHolder.Relocation();
        _bezierCurve.gameObject.SetActive(false);
    }


    protected override void Use()
    {
        base.Use();
        BattleManager.Instance.TargetEnemy.Hit(5);
    }
}
