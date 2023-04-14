using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;

    private Coroutine _coMove;

    [SerializeField]
    private float _moveTime = 0.2f;   // 움직이는 시간
    [SerializeField]
    private float _upPosition = 100f;  // 마우스 오버 시 올라가는 정도
    [SerializeField]
    private float _overScale = 1.3f;  // 마우스 오버 시 커지는 정도

    protected CardHolder CardHolder => GameManager.Game.CardHolder;


    // 해당 그림에 마우스를 댈 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardHolder.isDrag)
            return;

        CardHolder.OverCard(this);
    }

    // 해당 그림에 마우스가 떠날 때
    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardHolder.isDrag)
            return;

        CardHolder.Relocation();
    }


    // 카드 이동
    public void MoveCard(Vector3 targetPos, Vector3 targetRot, Vector3 targetScl)
    {
        _targetPos = targetPos;
        _targetRot = targetRot;
        _targetScl = targetScl;

        if(_coMove != null)
        {
            StopCoroutine(_coMove);
        }
        _coMove = StartCoroutine(CoMove());
    }

    protected void UseCardMove()
    {
        // 사용하고 그 카드는 버림
        Use();
        CardHolder.DiscardCard(this);
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

    public void SetActiveRaycast(bool flag)
    {
        Image[] children = GetComponentsInChildren<Image>();
        foreach (Image child in children)
        {
            child.raycastTarget = flag;
        }
    }

    protected virtual void Use()
    {
        Debug.Log("사용합니다.");
    }
}
