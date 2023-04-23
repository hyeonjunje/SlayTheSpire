using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private float _moveTime = 0.2f;   // 움직이는 시간

    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;

    private Coroutine _coMove;
    private BaseCard _baseCard;

    public bool IsBezierCurve { get; private set; }

    public void Init(bool isBezierCurve , BaseCard baseCard)
    {
        // BaseCard도 여기서 넘겨주자
        IsBezierCurve = isBezierCurve;
        _baseCard = baseCard;
    }

    public void StopAllCoroutine()
    {
        StopAllCoroutines();
    }

    // 카드를 드래그 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnDrag(eventData);
    }

    // 드래그를 끝낼 때(드래그할 수 있는 곳이 아니여도 됨)
    public void OnEndDrag(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnEndDrag(eventData);
    }

    // 해당 카드에 마우스를 호버할 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnPointerEnter(eventData);
    }

    // 해당 카드에 클릭했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnPointerClick(eventData);
    }

    // 해당 카드에서 마우스를 나갈 때
    public void OnPointerExit(PointerEventData eventData)
    {
        _baseCard.CurrentState.OnPointerExit(eventData);
    }

    // 카드 이동
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
