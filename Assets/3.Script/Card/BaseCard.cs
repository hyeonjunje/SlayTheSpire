using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;
    protected Coroutine _coMove;

    [SerializeField]
    private float _moveTime = 0.2f;

    [SerializeField]
    private float _upPosition = 100f;

    private Vector3 _prevRot;

    protected bool _isDrag = false;
    protected CardHolder _cardHolder => GameManager.Game.CardHolder;


    // �ش� �׸��� ���콺�� �� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �ش� ī�� Ŀ��
        // �� ���� ī�� �̵�
        _prevRot = _targetRot;

        _targetPos += new Vector3(0, _upPosition, 0);
        _targetRot = Vector3.zero;
        _targetScl = Vector3.one * 1.5f;

        MoveCard();
        _cardHolder.OnPointerEnterCardHand(this);
    }

    // �ش� �׸��� ���콺�� ���� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isDrag)
            return;

        _targetPos -= new Vector3(0, _upPosition, 0);
        _targetRot = _prevRot;
        _targetScl = Vector3.one * 1f;

        _cardHolder.DisplayMyHand();
    }


    // ī�带 ���� ��
    public void MoveCard(Vector3 targetPos = default(Vector3), Vector3 targetRot = default(Vector3), Vector3 targetScl = default(Vector3))
    {
        if (_isDrag)
            return;

        if (targetPos != default(Vector3))
            _targetPos = targetPos;
        if (targetRot != default(Vector3))
            _targetRot = targetRot;
        if (targetScl != default(Vector3))
            _targetScl = targetScl;

        if(_coMove != null)
        {
            StopCoroutine(_coMove);
        }
        _coMove = StartCoroutine(CoMove());
    }

    protected void ClearCoroutine()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoMove()
    {
        float currentTime = 0f;

        Vector3 originPos = transform.localPosition;
        Vector3 originRot = transform.localEulerAngles;
        Vector3 originScl = transform.localScale;

        while (true)
        {
            currentTime += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(originPos, _targetPos, currentTime / _moveTime);
            transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(originRot.z, _targetRot.z, currentTime / _moveTime));
            transform.localScale = Vector3.Lerp(originScl, _targetScl, currentTime / _moveTime);

            if (currentTime >= _moveTime)
                break;

            yield return null;
        }
    }
}
