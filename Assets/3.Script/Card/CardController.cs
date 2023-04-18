using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isDrag = false;

    [SerializeField]
    private float _moveTime = 0.2f;   // �����̴� �ð�

    private bool _isBezierCurve = false;

    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;

    private Coroutine _coMove;

    private BaseCard _baseCard;
    private CardHolder _cardHolder;
    private BezierCurve _bezierCurve;

    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();

    public void Init(CardHolder cardHolder, bool isBezierCurve , BaseCard baseCard)
    {
        // isBattle�̳� isDrag ó��
        // BaseCard�� ���⼭ �Ѱ�����
        isDrag = false;

        _isBezierCurve = isBezierCurve;

        _cardHolder = cardHolder;
        _bezierCurve = cardHolder.BezierCurve;

        _baseCard = baseCard;
    }

    // ī�带 �巡�� ������ ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_baseCard.cardUsage == ECardUsage.Battle)
        {
            _cardHolder.isDrag = true;
            _cardHolder.selectedCard = _baseCard;

            StopAllCoroutines();

            transform.SetAsLastSibling();

            if (_isBezierCurve)
            {
                _bezierCurve.gameObject.SetActive(true);
                _cardHolder.MoveCenter(_baseCard);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_baseCard.cardUsage == ECardUsage.Battle)
        {
            if (_isBezierCurve)
            {
                _bezierCurve.p0.position = transform.position;
                _bezierCurve.p2.position = eventData.position;
            }
            else
            {
                transform.position = eventData.position;
            }
        }
    }

    // �巡�׸� ���� ��(�巡���� �� �ִ� ���� �ƴϿ��� ��)
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_baseCard.cardUsage == ECardUsage.Battle)
        {
            _cardHolder.isDrag = false;

            if (_isBezierCurve) // Ÿ���� ������
            {
                // Ÿ���� ���� ���� ���
                if (battleManager.TargetEnemy != null)
                {
                    SetActiveRaycast(false);
                    _baseCard.UseCard();
                }

                // ������ ���� �� nulló��
                battleManager.TargetEnemy = null;

                _bezierCurve.gameObject.SetActive(false);
            }
            else
            {
                // ��밡��(�ڽ�Ʈ ���)�̰ų� �������� ���� ���� ���
                // ��� ���� y���� 300�̻� -> �̴� �ػ󵵿� ���� �ٲٴ� ������ �ʿ�
                if (eventData.position.y > 300f)
                {
                    SetActiveRaycast(false);
                    _baseCard.UseCard();
                }
            }

            // �Ұ� �� �ϰ� nulló��
            _cardHolder.selectedCard = null;
            // ������ �� ������ � ��Ȱ��ȭ
            _cardHolder.Relocation();
        }
    }

    // �ش� ī�忡 ���콺�� ȣ���� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_baseCard.cardUsage == ECardUsage.Battle)
        {
            if (_cardHolder.isDrag)
                return;

            _cardHolder.OverCard(_baseCard);
        }
    }

    // �ش� ī�忡 Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        _baseCard.onClickAction?.Invoke();

        // ���� �� �ִ� ī���� ��� �� ���� ī�带 �߰�
        if(_baseCard.cardUsage == ECardUsage.Gain)
        {
            battleManager.Player.AddCard(_baseCard);
        }
    }

    // �ش� ī�忡�� ���콺�� ���� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_baseCard.cardUsage == ECardUsage.Battle)
        {
            if (_cardHolder.isDrag)
                return;

            _cardHolder.Relocation();
        }
    }

    // ī�� �̵�
    public void MoveCard(Vector3 targetPos, Vector3 targetRot, Vector3 targetScl)
    {
        _targetPos = targetPos;
        _targetRot = targetRot;
        _targetScl = targetScl;

        if (_coMove != null)
        {
            StopCoroutine(_coMove);
        }
        _coMove = StartCoroutine(CoMove());
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

    public void SetActiveRaycast(bool flag)
    {
        Image[] children = GetComponentsInChildren<Image>();
        foreach (Image child in children)
        {
            child.raycastTarget = flag;
        }
    }
}
