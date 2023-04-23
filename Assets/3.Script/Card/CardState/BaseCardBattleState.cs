using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCardBattleState : BaseCardState
{
    private bool _isDrag = false;

    public BaseCardBattleState(BaseCard baseCard, BaseCardStateFactory stateFactory) : base(baseCard, stateFactory)
    {
        cardUsage = ECardUsage.Battle;
    }

    public override void Enter()
    {
        _isDrag = false;
    }

    public override void Exit()
    {

    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;

        _baseCard.CardHolder.selectedCard = _baseCard;

        _baseCard.CardController.StopAllCoroutine();

        _baseCard.transform.SetAsLastSibling();

        if (_baseCard.CardController.IsBezierCurve)
        {
            _baseCard.CardHolder.BezierCurve.gameObject.SetActive(true);
            _baseCard.CardHolder.MoveCenter(_baseCard);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_baseCard.CardController.IsBezierCurve)
        {
            _baseCard.CardHolder.BezierCurve.p0.position = _baseCard.transform.position;
            _baseCard.CardHolder.BezierCurve.p2.position = eventData.position;
        }
        else
        {
            _baseCard.transform.position = eventData.position;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;

        if (_baseCard.CardController.IsBezierCurve) // Ÿ���� ������
        {
            // Ÿ���� ���� ���� ���
            if (battleManager.TargetEnemy != null)
            {
                _baseCard.CardController.SetActiveRaycast(false);
                _baseCard.UseCard();
            }

            // ������ ���� �� nulló��
            battleManager.TargetEnemy = null;

            _baseCard.CardHolder.BezierCurve.gameObject.SetActive(false);
        }
        else
        {
            // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
            // ��� ���� y���� 300�̻� -> �̴� �ػ󵵿� ���� �ٲٴ� ������ �ʿ�
            if (eventData.position.y > 300f)
            {
                _baseCard.CardController.SetActiveRaycast(false);
                _baseCard.UseCard();
            }
        }

        // �Ұ� �� �ϰ� nulló��
        _baseCard.CardHolder.selectedCard = null;
        // ������ �� ������ � ��Ȱ��ȭ
        _baseCard.CardHolder.Relocation();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _baseCard.CardHolder.OverCard(_baseCard);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _baseCard.CardHolder.Relocation();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _baseCard.onClickAction?.Invoke();
    }
}
