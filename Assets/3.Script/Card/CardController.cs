using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isBattle = false;
    public bool isDrag = false;

    [SerializeField]
    private float _moveTime = 0.2f;   // 움직이는 시간

    private bool _isBezierCurve = false;

    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;

    private Coroutine _coMove;

    private BaseCard _baseCard;
    private CardHolder _cardHolder;
    private BezierCurve _bezierCurve;

    public void Init(CardHolder cardHolder, bool isBezierCurve , BaseCard baseCard)
    {
        // isBattle이나 isDrag 처리
        // BaseCard도 여기서 넘겨주자

        isBattle = false;
        isDrag = false;

        _isBezierCurve = isBezierCurve;

        _cardHolder = cardHolder;
        _bezierCurve = cardHolder.BezierCurve;

        _baseCard = baseCard;
    }

    // 카드를 드래그 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;
        _cardHolder.isDrag = true;
        _cardHolder.selectedCard = _baseCard;

        StopAllCoroutines();

        transform.SetAsLastSibling();

        if(_isBezierCurve) 
        {
            _bezierCurve.gameObject.SetActive(true);
            _cardHolder.MoveCenter(_baseCard);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;

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

    // 드래그를 끝낼 때(드래그할 수 있는 곳이 아니여도 됨)
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isBattle)
            return;
        _cardHolder.isDrag = false;

        if (_isBezierCurve) // 타겟이 있으면
        {
            // 타겟이 있을 때만 사용
            if (BattleManager.Instance.TargetEnemy != null)
            {
                SetActiveRaycast(false);
                _baseCard.UseCard();
            }

            // 때리고 나면 적 null처리
            BattleManager.Instance.TargetEnemy = null;

            _bezierCurve.gameObject.SetActive(false);
        }
        else
        {
            // 사용가능(코스트 등등)이거나 사용범위에 있을 때만 사용
            // 사용 범위 y값이 300이상 -> 이는 해상도에 따라 바꾸는 로직이 필요
            if (eventData.position.y > 300f)
            {
                SetActiveRaycast(false);
                _baseCard.UseCard();
            }
        }

        // 할거 다 하고 null처리
        _cardHolder.selectedCard = null;
        // 재정렬 후 베지어 곡선 비활성화
        _cardHolder.Relocation();
    }

    // 해당 카드에 마우스를 호버할 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 전투중이 아니라면(내 카드에서 볼 때...)
        if (!isBattle)
            return;

        if (_cardHolder.isDrag)
            return;

        _cardHolder.OverCard(_baseCard);
    }

    // 해당 카드에서 마우스를 나갈 때
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isBattle)
            return;

        if (_cardHolder.isDrag)
            return;

        _cardHolder.Relocation();
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
