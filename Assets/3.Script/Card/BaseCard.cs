using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler
{
    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _targetScl;
    private Coroutine _coMove;

    [SerializeField]
    private float _moveTime = 0.2f;


    // 마우스 클릭 시
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("이거 함? OnPointerClick " + gameObject.name);
    }

    // 마우스 누를 때
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("이거 함? OnPointerDown " + gameObject.name);
    }

    // 해당 그림에 마우스를 댈 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("이거 함? OnPointerEnter " + gameObject.name);
    }

    // 마우스 땔 때
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("이거 함? OnPointerUp " + gameObject.name);
    }


    // 카드를 뽑을 때
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
            // transform.localEulerAngles = Vector3.Lerp(originRot, _targetRot, currentTime / _moveTime);

            transform.localScale = Vector3.Lerp(originScl, _targetScl, currentTime / _moveTime);

            if (currentTime >= _moveTime)
                break;

            yield return null;
        }
    }
}
